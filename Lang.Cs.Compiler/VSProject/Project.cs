using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    
    property WorkingDirectory DirectoryInfo 
    
    property AssemblyName string nazwa DLL lub Exe
    
    property OutputType string 
    
    property ProjectGuid Guid 
    
    property ReferencedAssemblies List<Assembly> 
    	init #
    
    property Reference List<string> 
    	init #
    
    property ProjectReferences List<ProjectReference> 
    	init #
    
    property PropertyGroups List<PropertyGroup> 
    	init #
    
    property Items List<ProjectItem> 
    	init #
    smartClassEnd
    */

    public partial class Project
    {
        public Assembly[] LoadReferencedAssemblies(List<CompileResult> compiled)
        {
            List<Assembly> result = new List<Assembly>();
            foreach (var i in reference)
            {
                var aa = Assembly.LoadWithPartialName(i);
                result.Add(aa);
            }
            foreach (var projectReference in ProjectReferences)
            {
                var compiledProject = compiled.Where(i => i.ProjectGuid == projectReference.ProjectGuid).First();
                result.Add(compiledProject.CompiledAssembly);
            }


            return result.ToArray();
        }
        #region Static Methods

        // Public Methods 

        public static Project Load(string filename)
        {
            Project p = new Project();
            p.workingDirectory = new FileInfo(filename).Directory;
            XDocument doc = XDocument.Load(filename);
            var ns = doc.Root.Name.Namespace;

            foreach (var i in doc.Root.Elements(ns + "PropertyGroup"))
                p.ParsePropertyGroup(i);
            var ItemGroups = doc.Root.Elements(ns + "ItemGroup").ToArray();

            foreach (var ig in ItemGroups)
            {
                /*
                 *  <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Xml" />
                 */
                foreach (var i in ig.Elements())
                {
                    if (i.Name == ns + "Reference")
                    {
                        p.reference.Add((string)i.Attribute("Include"));
                    }
                    else if (i.Name == ns + "Compile")
                    {
                        var name = (string)i.Attribute("Include");
                        var pi = new ProjectItem(name, BuildActions.Compile);
                        p.items.Add(pi);

                    }
                    else if (i.Name == ns + "ProjectReference")
                    {
                        var g = ProjectReference.Deserialize(i);
                        p.projectReferences.Add(g);
                    }
                    else if (i.Name == ns + "Content")
                    {
                        var name = (string)i.Attribute("Include");
                        var pi = new ProjectItem(name, BuildActions.Content);
                        p.items.Add(pi);
                    }
                }
            }
            return p;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public CompileResult Compile(string outDir, string[] compiledReferences)
        {
            using (Microsoft.CSharp.CSharpCodeProvider provider = new Microsoft.CSharp.CSharpCodeProvider())
            {

                ICodeCompiler loCompiler = provider.CreateCompiler();
                CompilerParameters loParameters = new CompilerParameters();
                {
                    var imports = Reference.Select(Q => Q.Trim() + ".dll").Distinct().ToArray();
                    loParameters.ReferencedAssemblies.AddRange(imports);
                    if (compiledReferences != null && compiledReferences.Any())
                        loParameters.ReferencedAssemblies.AddRange(compiledReferences);
                }

                // loParameters.GenerateInMemory = true;
                loParameters.OutputAssembly = Path.Combine(outDir, assemblyName + ".dll");
                new FileInfo(loParameters.OutputAssembly).Directory.Create();
                // CompilerResults loCompiled = loCompiler.CompileAssemblyFromSource(loParameters, csCode);
                string[] codeBatch;
                {

                    List<string> codeBatchList = new List<string>();
                    foreach (var i in items.Where(i => i.BuildAction == BuildActions.Compile).Select(u => u.Name).Distinct())
                    {
                        var fn = Path.Combine(workingDirectory.FullName, i);
                        codeBatchList.Add(File.ReadAllText(fn));
                    }
                    // codeBatchList.Add(csCode);
                    codeBatch = codeBatchList.ToArray();
                }

                {
                    CompilerResults loCompiled = loCompiler.CompileAssemblyFromSourceBatch(loParameters, codeBatch);

                    if (loCompiled.Errors.HasErrors)
                    {
                        string lcErrorMsg = "";
                        lcErrorMsg = loCompiled.Errors.Count.ToString() + " Errors:";
                        for (int x = 0; x < loCompiled.Errors.Count; x++)
                            lcErrorMsg = lcErrorMsg + "\r\nLine: " + loCompiled.Errors[x].Line.ToString() + " - " +
                                         loCompiled.Errors[x].ErrorText;
                        // MessageBox.Show(lcErrorMsg + "\r\n\r\n" + lcCode, "Compiler Demo");
                        // throw new Exception(lcErrorMsg + "\r\n\r\n" + csCode);
                        var g = loCompiled.Errors.OfType<CompilerError>().Where(i => !i.IsWarning).ToArray();
                        throw new Exception("Compile error: " + g.First().ErrorText);
                    }
                    var a = new CompileResult();
                    a.CompiledAssembly = loCompiled.CompiledAssembly;
                    a.OutputAssemblyFilename = loParameters.OutputAssembly;
                    a.ProjectGuid = ProjectGuid;
                    return a;
                }
            }
        }
        // Private Methods 

        void ParsePropertyGroup(XElement xElement)
        {
            var a = PropertyGroup.Deserialize(xElement);
            if (!string.IsNullOrEmpty(a.OutputType))
            {
                outputType = a.OutputType;
                assemblyName = a.AssemblyName;
                if (a.ProjectGuid.HasValue)
                    projectGuid = a.ProjectGuid.Value;
            }
            else
            {
                PropertyGroups.Add(a);
            }
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-04 15:48
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class Project
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Project()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##WorkingDirectory## ##AssemblyName## ##OutputType## ##ProjectGuid## ##ReferencedAssemblies## ##Reference## ##ProjectReferences## ##PropertyGroups## ##Items##
        implement ToString WorkingDirectory=##WorkingDirectory##, AssemblyName=##AssemblyName##, OutputType=##OutputType##, ProjectGuid=##ProjectGuid##, ReferencedAssemblies=##ReferencedAssemblies##, Reference=##Reference##, ProjectReferences=##ProjectReferences##, PropertyGroups=##PropertyGroups##, Items=##Items##
        implement equals WorkingDirectory, AssemblyName, OutputType, ProjectGuid, ReferencedAssemblies, Reference, ProjectReferences, PropertyGroups, Items
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności WorkingDirectory; 
        /// </summary>
        public const string PROPERTYNAME_WORKINGDIRECTORY = "WorkingDirectory";
        /// <summary>
        /// Nazwa własności AssemblyName; nazwa DLL lub Exe
        /// </summary>
        public const string PROPERTYNAME_ASSEMBLYNAME = "AssemblyName";
        /// <summary>
        /// Nazwa własności OutputType; 
        /// </summary>
        public const string PROPERTYNAME_OUTPUTTYPE = "OutputType";
        /// <summary>
        /// Nazwa własności ProjectGuid; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTGUID = "ProjectGuid";
        /// <summary>
        /// Nazwa własności ReferencedAssemblies; 
        /// </summary>
        public const string PROPERTYNAME_REFERENCEDASSEMBLIES = "ReferencedAssemblies";
        /// <summary>
        /// Nazwa własności Reference; 
        /// </summary>
        public const string PROPERTYNAME_REFERENCE = "Reference";
        /// <summary>
        /// Nazwa własności ProjectReferences; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTREFERENCES = "ProjectReferences";
        /// <summary>
        /// Nazwa własności PropertyGroups; 
        /// </summary>
        public const string PROPERTYNAME_PROPERTYGROUPS = "PropertyGroups";
        /// <summary>
        /// Nazwa własności Items; 
        /// </summary>
        public const string PROPERTYNAME_ITEMS = "Items";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo WorkingDirectory
        {
            get
            {
                return workingDirectory;
            }
            set
            {
                workingDirectory = value;
            }
        }
        private DirectoryInfo workingDirectory;
        /// <summary>
        /// nazwa DLL lub Exe
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                assemblyName = value;
            }
        }
        private string assemblyName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OutputType
        {
            get
            {
                return outputType;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                outputType = value;
            }
        }
        private string outputType = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Guid ProjectGuid
        {
            get
            {
                return projectGuid;
            }
            set
            {
                projectGuid = value;
            }
        }
        private Guid projectGuid;
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
            set
            {
                referencedAssemblies = value;
            }
        }
        private List<Assembly> referencedAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public List<string> Reference
        {
            get
            {
                return reference;
            }
            set
            {
                reference = value;
            }
        }
        private List<string> reference = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public List<ProjectReference> ProjectReferences
        {
            get
            {
                return projectReferences;
            }
            set
            {
                projectReferences = value;
            }
        }
        private List<ProjectReference> projectReferences = new List<ProjectReference>();
        /// <summary>
        /// 
        /// </summary>
        public List<PropertyGroup> PropertyGroups
        {
            get
            {
                return propertyGroups;
            }
            set
            {
                propertyGroups = value;
            }
        }
        private List<PropertyGroup> propertyGroups = new List<PropertyGroup>();
        /// <summary>
        /// 
        /// </summary>
        public List<ProjectItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
        private List<ProjectItem> items = new List<ProjectItem>();
        #endregion Properties

    }
}
