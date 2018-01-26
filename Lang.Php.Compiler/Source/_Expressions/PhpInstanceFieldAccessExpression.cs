using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpInstanceFieldAccessExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="fieldName"></param>
        ///     <param name="targetObject"></param>
        ///     <param name="includeModule"></param>
        /// </summary>
        public PhpInstanceFieldAccessExpression(string fieldName, IPhpValue targetObject,
            PhpCodeModuleName                          includeModule)
        {
            FieldName     = fieldName;
            TargetObject  = targetObject;
            IncludeModule = includeModule;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests(TargetObject).ToList();
            if (IncludeModule != null)
                a.Add(new ModuleCodeRequest(IncludeModule, string.Format("instance field {0}", this)));
            return a;
        }
        // Public Methods 

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}->{1}", TargetObject.GetPhpCode(style), FieldName);
        }

        public override string ToString()
        {
            return GetPhpCode(new PhpEmitStyle());
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string FieldName { get; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue TargetObject { get; }

        /// <summary>
        /// </summary>
        public PhpCodeModuleName IncludeModule { get; set; }
    }
}