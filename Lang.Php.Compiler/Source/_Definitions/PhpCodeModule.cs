using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Name
    
    property Name PhpCodeModuleName nazwa pliku
    
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

    public partial class PhpCodeModule : ICodeRelated
    {
        #region Methods

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpEmitStyle style, string filename)
        {

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            PhpSourceCodeWriter writer = new PhpSourceCodeWriter();
            var style_CurrentNamespace = style.CurrentNamespace;
            try
            {
                style.CurrentNamespace = "";
                if (!string.IsNullOrEmpty(this.topComments))
                    writer.WriteLn("/*\r\n" + topComments.Trim() + "\r\n*/");
                // var noBracketStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);
                //_module = module;
                //_context = context;
                var module = this;
                {
                    var namespaces = module.classes.Select(a => a.Name.Namespace).Distinct().ToArray();
                    var manyNamespaces = namespaces.Length > 1;
                    {
                        if (namespaces.Length == 1 && !string.IsNullOrEmpty(namespaces.First()))
                        {
                            style.CurrentNamespace = namespaces.First();
                            writer.WriteLnF("namespace {0};", style.CurrentNamespace.Substring(1));
                        }
                    }
                    var noBracketStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);
                    {
                        /// top code
                        PhpCodeBlock collectedTopCodeBlock = new PhpCodeBlock();
                        collectedTopCodeBlock.Statements.AddRange(ConvertRequestedToCode());
                        collectedTopCodeBlock.Statements.AddRange(ConvertDefinedConstToCode());
                        if (topCode != null)
                            collectedTopCodeBlock.Statements.AddRange(topCode.Statements);
                        if (collectedTopCodeBlock.Statements.Any())
                            collectedTopCodeBlock.Emit(emiter, writer, noBracketStyle);
                    }
                    #region Emit classes
                    {
                        if (manyNamespaces)
                        {
                            var classesGBNamespace = module.Classes.GroupBy(u => u.Name.Namespace);
                            foreach (var classesInNamespace in classesGBNamespace)
                            {
                                style.CurrentNamespace = classesInNamespace.Key;
                                try
                                {
                                    if (style.CurrentNamespace.Length > 0)
                                        writer.OpenLnF("namespace {0} {{", style.CurrentNamespace.Substring(1));
                                    else
                                        writer.OpenLn("namespace {");
                                    foreach (var cl in classesInNamespace)
                                        cl.Emit(emiter, writer, style);
                                    writer.CloseLn("}");
                                }
                                finally
                                {
                                    style.CurrentNamespace = "";
                                }
                            }
                        }
                        else
                            foreach (var cl in module.Classes)
                            {
                                cl.Emit(emiter, writer, style);
                            }
                    }
                    #endregion
                    if (bottomCode != null)
                        bottomCode.Emit(emiter, writer, noBracketStyle);
                }


                #region Save to file
                {
                    FileInfo fi = new FileInfo(filename);
                    fi.Directory.Create();
                    var codeStr = writer.GetCode();
                    var binary = Encoding.UTF8.GetBytes(codeStr);
                    File.WriteAllBytes(fi.FullName, binary);
                }
                #endregion
            }
            finally
            {
                style.CurrentNamespace = style_CurrentNamespace;
            }
        }

        public PhpClassDefinition FindOrCreateClass(PhpQualifiedName PhpClassName, PhpQualifiedName baseClass)
        {
            var c = classes.Where(i => PhpClassName == i.Name).FirstOrDefault();
            if (c == null)
            {
                c = new PhpClassDefinition(PhpClassName, baseClass);
                classes.Add(c);
            }
            return c;
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.XXX(topCode, bottomCode);
            var b = IPhpStatementBase.XXX<PhpClassDefinition>(classes);
            return a.Union(b);
        }
        // Private Methods 

        private IPhpStatement[] ConvertDefinedConstToCode()
        {
            List<IPhpStatement> result = new List<IPhpStatement>();

            List<string> alreadyDefined = new List<string>();
            if (definedConsts.Any())
            {
                var grouped = definedConsts.GroupBy(u => GetNamespace(u.Key)).ToArray();

                bool useNamespaces = grouped.Length > 1 || grouped[0].Key != PathUtil.UNIX_SEP;
                foreach (var group in grouped)
                {
                    List<IPhpStatement> container;
                    if (useNamespaces)
                    {
                        var ns1 = new PhpNamespaceStatement(group.Key);
                        container = ns1.Code.Statements;
                        result.Add(ns1);
                    }
                    else
                        container = result;
                    foreach (var item in group)
                    {
                        var shortName = GetShortName(item.Key);
                        if (alreadyDefined.Contains(item.Key))
                            continue;
                        alreadyDefined.Add(item.Key);
                        var defined = new PhpMethodCallExpression("defined", new PhpConstValue(shortName));
                        var notDefined = new PhpUnaryOperatorExpression(defined, "!");
                        var define = new PhpMethodCallExpression("define", new PhpConstValue(shortName), item.Value);
                        PhpIfStatement ifStatement = new PhpIfStatement(notDefined, new PhpExpressionStatement(define), null);
                        container.Add(ifStatement);
                    }
                }
            }
            return result.ToArray();
        }

        private IPhpStatement[] ConvertRequestedToCode()
        {
            List<IPhpStatement> result = new List<IPhpStatement>();
            List<string> alreadyDefined = new List<string>();
            PhpEmitStyle style = new PhpEmitStyle();
            foreach (var item in requiredFiles.Distinct())
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

        string GetNamespace(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP);
            return a.Substring(0, g);
        }

        string GetShortName(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP);
            return a.Substring(g + 1);
        }

        #endregion Methods

        #region Properties

        public bool IsEmpty
        {
            get
            {
                foreach (var i in classes)
                    if (!i.IsEmpty)
                        return false;
                return definedConsts.Count == 0 && !PhpCodeBlock.HasAny(topCode) && !PhpCodeBlock.HasAny(bottomCode);
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-06 11:48
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="Name">nazwa pliku</param>
        /// </summary>
        public PhpCodeModule(PhpCodeModuleName Name)
        {
            this.Name = Name;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa pliku
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności TopComments; komentarz na szczycie pliku
        /// </summary>
        public const string PROPERTYNAME_TOPCOMMENTS = "TopComments";
        /// <summary>
        /// Nazwa własności TopCode; 
        /// </summary>
        public const string PROPERTYNAME_TOPCODE = "TopCode";
        /// <summary>
        /// Nazwa własności BottomCode; 
        /// </summary>
        public const string PROPERTYNAME_BOTTOMCODE = "BottomCode";
        /// <summary>
        /// Nazwa własności Classes; classes in this module
        /// </summary>
        public const string PROPERTYNAME_CLASSES = "Classes";
        /// <summary>
        /// Nazwa własności RequiredFiles; Pliki dołączane do require
        /// </summary>
        public const string PROPERTYNAME_REQUIREDFILES = "RequiredFiles";
        /// <summary>
        /// Nazwa własności DefinedConsts; 
        /// </summary>
        public const string PROPERTYNAME_DEFINEDCONSTS = "DefinedConsts";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// nazwa pliku
        /// </summary>
        public PhpCodeModuleName Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private PhpCodeModuleName name;
        /// <summary>
        /// komentarz na szczycie pliku
        /// </summary>
        public string TopComments
        {
            get
            {
                return topComments;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                topComments = value;
            }
        }
        private string topComments = "Generated with CS2PHP";
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeBlock TopCode
        {
            get
            {
                return topCode;
            }
            set
            {
                topCode = value;
            }
        }
        private PhpCodeBlock topCode = new PhpCodeBlock();
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeBlock BottomCode
        {
            get
            {
                return bottomCode;
            }
            set
            {
                bottomCode = value;
            }
        }
        private PhpCodeBlock bottomCode = new PhpCodeBlock();
        /// <summary>
        /// classes in this module; własność jest tylko do odczytu.
        /// </summary>
        public List<PhpClassDefinition> Classes
        {
            get
            {
                return classes;
            }
        }
        private List<PhpClassDefinition> classes = new List<PhpClassDefinition>();
        /// <summary>
        /// Pliki dołączane do require; własność jest tylko do odczytu.
        /// </summary>
        public List<IPhpValue> RequiredFiles
        {
            get
            {
                return requiredFiles;
            }
        }
        private List<IPhpValue> requiredFiles = new List<IPhpValue>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public List<KeyValuePair<string, IPhpValue>> DefinedConsts
        {
            get
            {
                return definedConsts;
            }
        }
        private List<KeyValuePair<string, IPhpValue>> definedConsts = new List<KeyValuePair<string, IPhpValue>>();
        #endregion Properties
    }
}
