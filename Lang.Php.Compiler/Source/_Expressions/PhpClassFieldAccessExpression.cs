using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpClassFieldAccessExpression : PhpValueBase
    {
        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (_className.EmitName != PhpQualifiedName.ClassnameSelf)
                yield return new ClassCodeRequest(_className);
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}::{1}{2}",
                _className.NameForEmit(style),
                IsConst ? "" : "$",
                _fieldName);
        }

        public void SetClassName(PhpQualifiedName phpClassName, ClassTranslationInfo classTi)
        {
            _className = phpClassName;
            ClassTi    = classTi;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName ClassName => _className;

        private PhpQualifiedName _className;

        /// <summary>
        /// </summary>
        public string FieldName
        {
            get => _fieldName;
            set
            {
                value                               = (value ?? string.Empty).Trim();
                while (value.StartsWith("$")) value = value.Substring(1);
                _fieldName                          = value;
            }
        }

        private string _fieldName = string.Empty;

        /// <summary>
        /// </summary>
        public bool IsConst { get; set; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public ClassTranslationInfo ClassTi { get; private set; }
    }
}