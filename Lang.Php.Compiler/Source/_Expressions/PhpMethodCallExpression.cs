using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{

    public  class PhpMethodCallExpression : PhpValueBase
    {
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(string name, params IPhpValue[] args)
        {
            _name = name;
            Arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(IPhpValue targetObject, string name, params IPhpValue[] args)
        {
            _name = name;
            Arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
            TargetObject = targetObject;
        }

        // Public Methods 


        public static PhpMethodCallExpression MakeConstructor(string constructedClassName, MethodTranslationInfo translationInfo, params IPhpValue[] args)
        {
            var methodCallExpression = new PhpMethodCallExpression(ConstructorMethodName, args);
            methodCallExpression.SetClassName((PhpQualifiedName)constructedClassName, translationInfo);
            return methodCallExpression;
        }

        public void SetClassName(PhpQualifiedName className, MethodTranslationInfo translationInfo)
        {
            _className = className.MakeAbsolute();
            TranslationInfo = translationInfo;
        }

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var requests = PhpStatementBase.GetCodeRequests(Arguments.Select(i => i.Expression)).ToList();
            if (!_className.IsEmpty && !DontIncludeClass && _className.EmitName != PhpQualifiedName.ClassnameSelf)
                requests.Add(new ClassCodeRequest(_className));
            return requests;
        }

        public override string ToString()
        {
            return GetPhpCode(new PhpEmitStyle());
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var join = style == null || style.Compression == EmitStyleCompression.Beauty ? ", " : ",";
            var xstyle = PhpEmitStyle.xClone(style);
            var arguments = string.Join(join, Arguments.Select(i => i.GetPhpCode(xstyle)));
            if (IsConstructorCall)
            {
                var a = string.Format("new {0}({1})", _className.NameForEmit(style), arguments);
                return a;
            }
            var name = _name;
            if (!_className.IsEmpty)
                name = _className.NameForEmit(style) + "::" + name;
            else if (TargetObject != null)
            {
                var to = TargetObject;
                if (TargetObject is PhpMethodCallExpression && (TargetObject as PhpMethodCallExpression).IsConstructorCall)
                    to = new PhpParenthesizedExpression(to);
                name = to.GetPhpCode(style) + "->" + name;
            }
            var code = string.Format(name == "echo" ? "{0} {1}" : "{0}({1})", name, arguments);
            return code;
        }

        public const string ConstructorMethodName = "*";

        public MethodCallStyles CallType
        {
            get
            {
                if (TargetObject != null)
                    return MethodCallStyles.Instance;
                return _className.IsEmpty ? MethodCallStyles.Procedural : MethodCallStyles.Static;
            }
        }
    
     
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(string name)
        {
            Name = name;
        }
 
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = (value ?? string.Empty).Trim();
        }
        private string _name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpMethodInvokeValue> Arguments { get; set; } = new List<PhpMethodInvokeValue>();

        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsConstructorCall => _name == ConstructorMethodName;

        /// <summary>
        /// 
        /// </summary>
        public IPhpValue TargetObject { get; set; }

        /// <summary>
        /// Nazwa klasy dla konstruktora lub metody statycznej; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName ClassName => _className;

        private PhpQualifiedName _className;
        /// <summary>
        /// indicates that method is from standard PHP class or other framework i.e. Wordpress
        /// </summary>
        public bool DontIncludeClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MethodTranslationInfo TranslationInfo { get; set; }
    }
}
