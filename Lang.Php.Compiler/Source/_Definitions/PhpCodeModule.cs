using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Name
    implement ToString Module ##Name##
    
    property Name PhpCodeModuleName nazwa pliku
    	read only
    
    property TopComments string komentarz na szczycie pliku
    	init "Generated with CS2PHP"
    
    property TopCode PhpCodeBlock 
    	init #
    
    property BottomCode PhpCodeBlock 
    	init #
    
    property Classes List<PhpClassDefinition> classes in this module
    	init #
    	read only
    
    property RequiredFiles List<IPhpValue> Pliki dołączane do require
    	init #
    	read only
    
    property DefinedConsts List<KeyValuePair<string,IPhpValue>> 
    	init #
    	read only
    smartClassEnd
    */

    public partial class PhpCodeModule : ICodeRelated, IEmitable
    {
        #region Static Methods

        // Private Methods 

        private static void EmitWithNamespace(PhpNamespace ns, PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style, IEnumerable<IEmitable> classesInNamespace)
        {
            if (classesInNamespace == null)
                return;
            var inNamespace = classesInNamespace as IEmitable[] ?? classesInNamespace.ToArray();
            if (!inNamespace.Any())
                return;
            style.CurrentNamespace = ns;
            try
            {
                if (ns.IsRoot)
                    writer.OpenLn("namespace {");
                else
                    writer.OpenLnF("namespace {0} {{", ns.Name.Substring(1));
                foreach (var cl in inNamespace)
                    cl.Emit(emiter, writer, style);
                writer.CloseLn("}");
            }
            finally
            {
                style.CurrentNamespace = null;
            }
        }

        static string GetNamespace(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP, StringComparison.Ordinal);
            return a.Substring(0, g);
        }

        static string GetShortName(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP, StringComparison.Ordinal);
            return a.Substring(g + 1);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpEmitStyle style, string filename)
        {

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            var writer = new PhpSourceCodeWriter();
            var styleCurrentNamespace = style.CurrentNamespace;
            try
            {
                Emit(emiter, writer, style);

                #region Save to file
                {
                    var fi = new FileInfo(filename);
                    if (fi.Directory != null) fi.Directory.Create();
                    var codeStr = writer.GetCode();
                    var binary = Encoding.UTF8.GetBytes(codeStr);
                    File.WriteAllBytes(fi.FullName, binary);
                }
                #endregion
            }
            finally
            {
                style.CurrentNamespace = styleCurrentNamespace;
            }
        }

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var nsManager = new PhpModuleNamespaceManager();
            style.CurrentNamespace = null;
            if (!string.IsNullOrEmpty(_topComments))
                writer.WriteLn("/*\r\n" + _topComments.Trim() + "\r\n*/");
            var module = this;
            {
                // var noBracketStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);

                #region Top code

                {
                    // top code
                    var collectedTopCodeBlock = new PhpCodeBlock();
                    collectedTopCodeBlock.Statements.AddRange(ConvertRequestedToCode());
                    collectedTopCodeBlock.Statements.AddRange(ConvertDefinedConstToCode());
                    if (_topCode != null)
                        collectedTopCodeBlock.Statements.AddRange(_topCode.Statements);
                    nsManager.Add(collectedTopCodeBlock.Statements);
                }

                #endregion

                {
                    var classesGbNamespace = module.Classes.GroupBy(u => u.Name.Namespace);
                    foreach (var classesInNamespace in classesGbNamespace.OrderBy(i => !i.Key.IsRoot))
                        foreach (var c in classesInNamespace)
                            nsManager.Add(c);
                }
                if (_bottomCode != null)
                    nsManager.Add(_bottomCode.Statements);
                if (!nsManager.Container.Any()) 
                    return;
                if (nsManager.OnlyOneRootStatement)
                    foreach (var cl in nsManager.Container[0].Items)
                        cl.Emit(emiter, writer, style);
                else
                    foreach (var ns in nsManager.Container)
                        EmitWithNamespace(ns.Name, emiter, writer, style, ns.Items);
            }
        }

        public PhpClassDefinition FindOrCreateClass(PhpQualifiedName phpClassName, PhpQualifiedName baseClass)
        {
            var c = _classes.FirstOrDefault(i => phpClassName == i.Name);
            if (c != null) return c;
            c = new PhpClassDefinition(phpClassName, baseClass);
            _classes.Add(c);
            return c;
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.GetCodeRequests(_topCode, _bottomCode);
            var b = IPhpStatementBase.GetCodeRequests(_classes);
            return a.Union(b);
        }
        // Private Methods 

        private IEnumerable<IPhpStatement> ConvertDefinedConstToCode()
        {
            var result = new List<IPhpStatement>();
            var alreadyDefined = new List<string>();
            if (!_definedConsts.Any()) return result.ToArray();
            var grouped = _definedConsts.GroupBy(u => GetNamespace(u.Key)).ToArray();

            var useNamespaces = grouped.Length > 1 || grouped[0].Key != PathUtil.UNIX_SEP;
            foreach (var group in grouped)
            {
                List<IPhpStatement> container;
                if (useNamespaces)
                {
                    var ns1 = new PhpNamespaceStatement((PhpNamespace)@group.Key);
                    container = ns1.Code.Statements;
                    result.Add(ns1);
                }
                else
                    container = result;
                foreach (var item in @group)
                {
                    var shortName = GetShortName(item.Key);
                    if (alreadyDefined.Contains(item.Key))
                        continue;
                    alreadyDefined.Add(item.Key);
                    var defined = new PhpMethodCallExpression("defined", new PhpConstValue(shortName));
                    var notDefined = new PhpUnaryOperatorExpression(defined, "!");
                    var define = new PhpMethodCallExpression("define", new PhpConstValue(shortName), item.Value);
                    var ifStatement = new PhpIfStatement(notDefined, new PhpExpressionStatement(define), null);
                    container.Add(ifStatement);
                }
            }
            return result.ToArray();
        }

        private IEnumerable<IPhpStatement> ConvertRequestedToCode()
        {
            var result = new List<IPhpStatement>();
            var alreadyDefined = new List<string>();
            var style = new PhpEmitStyle();
            foreach (var item in _requiredFiles.Distinct())
            {
                var code = item.GetPhpCode(style); //rozróżniam je po wygenerowanym kodzie
                if (alreadyDefined.Contains(code))
                    continue;
                alreadyDefined.Add(code);
                var req = new PhpMethodCallExpression("require_once", item);
                result.Add(new PhpExpressionStatement(req));
            }
            return result.ToArray();
        }

        #endregion Methods

        #region Properties

        public bool IsEmpty
        {
            get
            {
                if (_classes.Any(i => !i.IsEmpty))
                    return false;
                return _definedConsts.Count == 0 && !PhpCodeBlock.HasAny(_topCode) && !PhpCodeBlock.HasAny(_bottomCode);
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-05 12:38
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpCodeModule
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpCodeModule()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##TopComments## ##TopCode## ##BottomCode## ##Classes## ##RequiredFiles## ##DefinedConsts##
        implement ToString Name=##Name##, TopComments=##TopComments##, TopCode=##TopCode##, BottomCode=##BottomCode##, Classes=##Classes##, RequiredFiles=##RequiredFiles##, DefinedConsts=##DefinedConsts##
        implement equals Name, TopComments, TopCode, BottomCode, Classes, RequiredFiles, DefinedConsts
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name">nazwa pliku</param>
        /// </summary>
        public PhpCodeModule(PhpCodeModuleName name)
        {
            _name = name;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa pliku
        /// </summary>
        public const string PropertyNameName = "Name";
        /// <summary>
        /// Nazwa własności TopComments; komentarz na szczycie pliku
        /// </summary>
        public const string PropertyNameTopComments = "TopComments";
        /// <summary>
        /// Nazwa własności TopCode; 
        /// </summary>
        public const string PropertyNameTopCode = "TopCode";
        /// <summary>
        /// Nazwa własności BottomCode; 
        /// </summary>
        public const string PropertyNameBottomCode = "BottomCode";
        /// <summary>
        /// Nazwa własności Classes; classes in this module
        /// </summary>
        public const string PropertyNameClasses = "Classes";
        /// <summary>
        /// Nazwa własności RequiredFiles; Pliki dołączane do require
        /// </summary>
        public const string PropertyNameRequiredFiles = "RequiredFiles";
        /// <summary>
        /// Nazwa własności DefinedConsts; 
        /// </summary>
        public const string PropertyNameDefinedConsts = "DefinedConsts";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("Module {0}", _name);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa pliku; własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName Name
        {
            get
            {
                return _name;
            }
        }
        private PhpCodeModuleName _name;
        /// <summary>
        /// komentarz na szczycie pliku
        /// </summary>
        public string TopComments
        {
            get
            {
                return _topComments;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _topComments = value;
            }
        }
        private string _topComments = "Generated with CS2PHP";
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeBlock TopCode
        {
            get
            {
                return _topCode;
            }
            set
            {
                _topCode = value;
            }
        }
        private PhpCodeBlock _topCode = new PhpCodeBlock();
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeBlock BottomCode
        {
            get
            {
                return _bottomCode;
            }
            set
            {
                _bottomCode = value;
            }
        }
        private PhpCodeBlock _bottomCode = new PhpCodeBlock();
        /// <summary>
        /// classes in this module; własność jest tylko do odczytu.
        /// </summary>
        public List<PhpClassDefinition> Classes
        {
            get
            {
                return _classes;
            }
        }
        private List<PhpClassDefinition> _classes = new List<PhpClassDefinition>();
        /// <summary>
        /// Pliki dołączane do require; własność jest tylko do odczytu.
        /// </summary>
        public List<IPhpValue> RequiredFiles
        {
            get
            {
                return _requiredFiles;
            }
        }
        private List<IPhpValue> _requiredFiles = new List<IPhpValue>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public List<KeyValuePair<string, IPhpValue>> DefinedConsts
        {
            get
            {
                return _definedConsts;
            }
        }
        private List<KeyValuePair<string, IPhpValue>> _definedConsts = new List<KeyValuePair<string, IPhpValue>>();
        #endregion Properties

    }
}
