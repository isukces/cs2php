using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Lang.Php.Compiler
{
    /// <summary>
    /// Part of code taken from http://stackoverflow.com/questions/6150644/change-default-app-config-at-runtime
    /// </summary>
    public class AppConfigManipulator : IDisposable
    {
        #region Constructors

        public AppConfigManipulator()
        {
            XDocument doc;
            if (File.Exists(_oldConfigFilename))
                doc = XDocument.Load(_oldConfigFilename);
            else
                doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
<startup><supportedRuntime version=""v4.0"" sku="".NETFramework,Version=v4.5""/></startup></configuration>");

            {
                var configuration = doc.Root;
                if (configuration != null)
                {
                    var runtime = configuration.Element("runtime");
                    if (runtime == null)
                        configuration.Add(runtime = new XElement("runtime"));
                    var nameAssemblyBinding = Namespace + "assemblyBinding";

                    var assemblyBinding = runtime.Element(nameAssemblyBinding);
                    if (assemblyBinding == null)
                        runtime.Add(assemblyBinding = new XElement(nameAssemblyBinding));
                    // assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1"
                    var an = typeof(PhpDummy).Assembly.GetName();
                    var pk = Bcd(an.GetPublicKeyToken());
                    var dependentAssembly = (from da in assemblyBinding.Elements(Namespace + "dependentAssembly")
                                             let tmp = DependentAssemblyWrapper.Parse(da)
                                             where tmp != null
                                             where tmp.Name == an.Name
                                             select tmp).FirstOrDefault();

                    if (dependentAssembly == null)
                    {
                        dependentAssembly = new DependentAssemblyWrapper
                        {
                            Name = an.Name,
                            PublicKeyToken = pk,
                        };
                        dependentAssembly.Update();
                        assemblyBinding.Add(dependentAssembly.DependentAssemblyElement);
                    }


                    dependentAssembly.RedirectOldVersion = "1.0.0.0-" + an.Version;
                    dependentAssembly.RedirectNewVersion = an.Version.ToString();
                    dependentAssembly.Update();
                }
            }

            path = string.Format("app{0:N}.config", Guid.NewGuid());
            path = Path.Combine(Path.GetTempPath(), path);
            doc.Save(path);
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", path);
            ResetConfigMechanism();
        }

        #endregion Constructors

        #region Static Methods

        // Private Methods 

        private static string Bcd(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private static void ResetConfigMechanism()
        {
            typeof(ConfigurationManager)
                .GetField("s_initState", BindingFlags.NonPublic |
                                         BindingFlags.Static)
                .SetValue(null, 0);

            typeof(ConfigurationManager)
                .GetField("s_configSystem", BindingFlags.NonPublic |
                                            BindingFlags.Static)
                .SetValue(null, null);

            typeof(ConfigurationManager)
                .Assembly.GetTypes()
                .Where(x => x.FullName ==
                            "System.Configuration.ClientConfigPaths")
                .First()
                .GetField("s_current", BindingFlags.NonPublic |
                                       BindingFlags.Static)
                .SetValue(null, null);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Dispose()
        {
            if (!_disposedValue)
            {
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", _oldConfigFilename);
                ResetConfigMechanism();

                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception e)
                    { }
                }
                _disposedValue = true;
            }
            GC.SuppressFinalize(this);
        }

        #endregion Methods

        #region Static Fields

        static readonly XNamespace Namespace = "urn:schemas-microsoft-com:asm.v1";

        #endregion Static Fields

        #region Fields

        private bool _disposedValue;
        private readonly string _oldConfigFilename =
        AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();
        private string path;

        #endregion Fields

        #region Nested Classes


        public class DependentAssemblyWrapper
        {
            #region Static Methods

            // Public Methods 

            public static DependentAssemblyWrapper Parse(XElement dependentAssembly)
            {
                var assemblyIdentity = dependentAssembly.Element(Namespace + "assemblyIdentity");
                if (assemblyIdentity == null)
                    return null;
                var x = new DependentAssemblyWrapper
                {
                    DependentAssemblyElement = dependentAssembly,
                    Name = (string)assemblyIdentity.Attribute("name"),
                    PublicKeyToken = (string)assemblyIdentity.Attribute("publicKeyToken"),
                    Culture = (string)assemblyIdentity.Attribute("culture")
                };
                return x;
            }

            #endregion Static Methods

            #region Methods

            // Public Methods 

            public void Update()
            {
                if (DependentAssemblyElement == null)
                    DependentAssemblyElement = new XElement(Namespace + "dependentAssembly");

                var ai = DependentAssemblyElement.Element(Namespace + "assemblyIdentity");
                if (ai == null)
                    DependentAssemblyElement.Add(ai = new XElement(Namespace + "assemblyIdentity"));

                ai.SetAttributeValue("name", Name);


                if (PublicKeyToken != null)
                    ai.SetAttributeValue("publicKeyToken", PublicKeyToken);
                if (Culture != null)
                    ai.SetAttributeValue("culture", Culture);

                var bi = DependentAssemblyElement.Element(Namespace + "bindingRedirect");
                if (bi == null)
                    DependentAssemblyElement.Add(bi = new XElement(Namespace + "bindingRedirect"));
                if (RedirectOldVersion != null)
                    bi.SetAttributeValue("oldVersion", RedirectOldVersion);
                if (RedirectNewVersion != null)
                    bi.SetAttributeValue("newVersion", RedirectNewVersion);
            }

            #endregion Methods

            #region Properties

            private string Culture { get; set; }

            public XElement DependentAssemblyElement { get; set; }

            public string Name { get; set; }

            public string PublicKeyToken { get; set; }

            public string RedirectNewVersion { get; set; }

            public string RedirectOldVersion { get; set; }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}
