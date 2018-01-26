using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lang.Php.Compiler.Source
{
    public class PhpCodeModule : ICodeRelated, IEmitable
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="name">nazwa pliku</param>
        /// </summary>
        public PhpCodeModule(PhpCodeModuleName name)
        {
            Name = name;
        }
        // Private Methods 

        private static void EmitWithNamespace(PhpNamespace ns, PhpSourceCodeEmiter emiter,
            PhpSourceCodeWriter                            writer,
            PhpEmitStyle                                   style, IEnumerable<IEmitable> classesInNamespace)
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

        private static string GetNamespace(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP, StringComparison.Ordinal);
            return a.Substring(0, g);
        }

        private static string GetShortName(string name)
        {
            var a = PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + name);
            var g = a.LastIndexOf(PathUtil.UNIX_SEP, StringComparison.Ordinal);
            return a.Substring(g + 1);
        }

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpEmitStyle style, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));

            var writer                = new PhpSourceCodeWriter();
            var styleCurrentNamespace = style.CurrentNamespace;
            try
            {
                Emit(emiter, writer, style);

                {
                    var fi = new FileInfo(filename);
                    if (fi.Directory != null) fi.Directory.Create();
                    var codeStr = writer.GetCode();
                    var binary  = Encoding.UTF8.GetBytes(codeStr);
                    File.WriteAllBytes(fi.FullName, binary);
                }
            }
            finally
            {
                style.CurrentNamespace = styleCurrentNamespace;
            }
        }

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var nsManager          = new PhpModuleNamespaceManager();
            style.CurrentNamespace = null;
            if (!string.IsNullOrEmpty(_topComments))
                writer.WriteLn("/*\r\n" + _topComments.Trim() + "\r\n*/");
            var module = this;
            {
                // var noBracketStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);

                {
                    // top code
                    var collectedTopCodeBlock = new PhpCodeBlock();
                    collectedTopCodeBlock.Statements.AddRange(ConvertRequestedToCode());
                    collectedTopCodeBlock.Statements.AddRange(ConvertDefinedConstToCode());
                    if (TopCode != null)
                        collectedTopCodeBlock.Statements.AddRange(TopCode.Statements);
                    nsManager.Add(collectedTopCodeBlock.Statements);
                }

                {
                    var classesGbNamespace = module.Classes.GroupBy(u => u.Name.Namespace);
                    foreach (var classesInNamespace in classesGbNamespace.OrderBy(i => !i.Key.IsRoot))
                    foreach (var c in classesInNamespace)
                        nsManager.Add(c);
                }
                if (BottomCode != null)
                    nsManager.Add(BottomCode.Statements);
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
            var c = Classes.FirstOrDefault(i => phpClassName == i.Name);
            if (c != null) return c;
            c = new PhpClassDefinition(phpClassName, baseClass);
            Classes.Add(c);
            return c;
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests(TopCode, BottomCode);
            var b = PhpStatementBase.GetCodeRequests(Classes);
            return a.Union(b);
        }

        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("Module {0}", Name);
        }
        // Private Methods 

        private IEnumerable<IPhpStatement> ConvertDefinedConstToCode()
        {
            var result         = new List<IPhpStatement>();
            var alreadyDefined = new List<string>();
            if (!DefinedConsts.Any()) return result.ToArray();
            var grouped = DefinedConsts.GroupBy(u => GetNamespace(u.Key)).ToArray();

            var useNamespaces = grouped.Length > 1 || grouped[0].Key != PathUtil.UNIX_SEP;
            foreach (var group in grouped)
            {
                List<IPhpStatement> container;
                if (useNamespaces)
                {
                    var ns1   = new PhpNamespaceStatement((PhpNamespace)group.Key);
                    container = ns1.Code.Statements;
                    result.Add(ns1);
                }
                else
                {
                    container = result;
                }

                foreach (var item in group)
                {
                    var shortName = GetShortName(item.Key);
                    if (alreadyDefined.Contains(item.Key))
                        continue;
                    alreadyDefined.Add(item.Key);
                    var defined     = new PhpMethodCallExpression("defined", new PhpConstValue(shortName));
                    var notDefined  = new PhpUnaryOperatorExpression(defined, "!");
                    var define      = new PhpMethodCallExpression("define", new PhpConstValue(shortName), item.Value);
                    var ifStatement = new PhpIfStatement(notDefined, new PhpExpressionStatement(define), null);
                    container.Add(ifStatement);
                }
            }

            return result.ToArray();
        }

        private IEnumerable<IPhpStatement> ConvertRequestedToCode()
        {
            var result         = new List<IPhpStatement>();
            var alreadyDefined = new List<string>();
            var style          = new PhpEmitStyle();
            foreach (var item in RequiredFiles.Distinct())
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

        public bool IsEmpty
        {
            get
            {
                if (Classes.Any(i => !i.IsEmpty))
                    return false;
                return DefinedConsts.Count == 0 && !PhpCodeBlock.HasAny(TopCode) && !PhpCodeBlock.HasAny(BottomCode);
            }
        }

        /// <summary>
        ///     nazwa pliku; własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName Name { get; }

        /// <summary>
        ///     komentarz na szczycie pliku
        /// </summary>
        public string TopComments
        {
            get => _topComments;
            set => _topComments = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public PhpCodeBlock TopCode { get; set; } = new PhpCodeBlock();

        /// <summary>
        /// </summary>
        public PhpCodeBlock BottomCode { get; set; } = new PhpCodeBlock();

        /// <summary>
        ///     classes in this module; własność jest tylko do odczytu.
        /// </summary>
        public List<PhpClassDefinition> Classes { get; } = new List<PhpClassDefinition>();

        /// <summary>
        ///     Pliki dołączane do require; własność jest tylko do odczytu.
        /// </summary>
        public List<IPhpValue> RequiredFiles { get; } = new List<IPhpValue>();

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public List<KeyValuePair<string, IPhpValue>> DefinedConsts { get; } =
            new List<KeyValuePair<string, IPhpValue>>();

        private string _topComments = "Generated with CS2PHP";
    }
}