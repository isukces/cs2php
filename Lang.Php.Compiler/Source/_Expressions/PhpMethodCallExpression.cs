using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Name
    
    property Name string 
    
    property Arguments List<PhpMethodInvokeValue> 
    	init #
    
    property IsConstructorCall bool 
    	read only _name == ConstructorMethodName
    
    property TargetObject IPhpValue 
    
    property ClassName PhpQualifiedName Nazwa klasy dla konstruktora lub metody statycznej
    	read only
    
    property DontIncludeClass bool indicates that method is from standard PHP class or other framework i.e. Wordpress
    
    property TranslationInfo MethodTranslationInfo 
    smartClassEnd
    */

    public partial class PhpMethodCallExpression : IPhpValueBase
    {
        #region Constructors



        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(string name, params IPhpValue[] args)
        {
            _name = name;
            _arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(IPhpValue targetObject, string name, params IPhpValue[] args)
        {
            _name = name;
            _arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
            _targetObject = targetObject;
        }

        #endregion Constructors

        #region Static Methods

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
            _translationInfo = translationInfo;
        }
        #endregion Static Methods

        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var requests = IPhpStatementBase.GetCodeRequests(_arguments.Select(i => i.Expression)).ToList();
            if (!_className.IsEmpty && !_dontIncludeClass && _className.EmitName != PhpQualifiedName.ClassnameSelf)
                requests.Add(new ClassCodeRequest(_className));
            return requests;
        }

        public override string ToString()
        {
            return GetPhpCode(new PhpEmitStyle());
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            string join = style == null || style.Compression == EmitStyleCompression.Beauty ? ", " : ",";
            var xstyle = PhpEmitStyle.xClone(style);
            var arguments = string.Join(join, _arguments.Select(i => i.GetPhpCode(xstyle)));
            if (IsConstructorCall)
            {
                var a = string.Format("new {0}({1})", _className.NameForEmit(style), arguments);
                return a;
            }
            var name = _name;
            if (!_className.IsEmpty)
                name = _className.NameForEmit(style) + "::" + name;
            else if (_targetObject != null)
            {
                var to = _targetObject;
                if (_targetObject is PhpMethodCallExpression && (_targetObject as PhpMethodCallExpression).IsConstructorCall)
                    to = new PhpParenthesizedExpression(to);
                name = to.GetPhpCode(style) + "->" + name;
            }
            var code = string.Format(name == "echo" ? "{0} {1}" : "{0}({1})", name, arguments);
            return code;
        }

        #endregion Methods

        #region Fields

        public const string ConstructorMethodName = "*";

        #endregion Fields

        #region Properties

        public MethodCallStyles CallType
        {
            get
            {
                if (_targetObject != null)
                    return MethodCallStyles.Instance;
                return _className.IsEmpty ? MethodCallStyles.Procedural : MethodCallStyles.Static;
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-26 17:26
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpMethodCallExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpMethodCallExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Arguments## ##IsConstructorCall## ##TargetObject## ##ClassName## ##DontIncludeClass## ##TranslationInfo##
        implement ToString Name=##Name##, Arguments=##Arguments##, IsConstructorCall=##IsConstructorCall##, TargetObject=##TargetObject##, ClassName=##ClassName##, DontIncludeClass=##DontIncludeClass##, TranslationInfo=##TranslationInfo##
        implement equals Name, Arguments, IsConstructorCall, TargetObject, ClassName, DontIncludeClass, TranslationInfo
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(string name)
        {
            Name = name;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; 
        /// </summary>
        public const string PropertyNameName = "Name";
        /// <summary>
        /// Nazwa własności Arguments; 
        /// </summary>
        public const string PropertyNameArguments = "Arguments";
        /// <summary>
        /// Nazwa własności IsConstructorCall; 
        /// </summary>
        public const string PropertyNameIsConstructorCall = "IsConstructorCall";
        /// <summary>
        /// Nazwa własności TargetObject; 
        /// </summary>
        public const string PropertyNameTargetObject = "TargetObject";
        /// <summary>
        /// Nazwa własności ClassName; Nazwa klasy dla konstruktora lub metody statycznej
        /// </summary>
        public const string PropertyNameClassName = "ClassName";
        /// <summary>
        /// Nazwa własności DontIncludeClass; indicates that method is from standard PHP class or other framework i.e. Wordpress
        /// </summary>
        public const string PropertyNameDontIncludeClass = "DontIncludeClass";
        /// <summary>
        /// Nazwa własności TranslationInfo; 
        /// </summary>
        public const string PropertyNameTranslationInfo = "TranslationInfo";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _name = value;
            }
        }
        private string _name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpMethodInvokeValue> Arguments
        {
            get
            {
                return _arguments;
            }
            set
            {
                _arguments = value;
            }
        }
        private List<PhpMethodInvokeValue> _arguments = new List<PhpMethodInvokeValue>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsConstructorCall
        {
            get
            {
                return _name == ConstructorMethodName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue TargetObject
        {
            get
            {
                return _targetObject;
            }
            set
            {
                _targetObject = value;
            }
        }
        private IPhpValue _targetObject;
        /// <summary>
        /// Nazwa klasy dla konstruktora lub metody statycznej; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName ClassName
        {
            get
            {
                return _className;
            }
        }
        private PhpQualifiedName _className;
        /// <summary>
        /// indicates that method is from standard PHP class or other framework i.e. Wordpress
        /// </summary>
        public bool DontIncludeClass
        {
            get
            {
                return _dontIncludeClass;
            }
            set
            {
                _dontIncludeClass = value;
            }
        }
        private bool _dontIncludeClass;
        /// <summary>
        /// 
        /// </summary>
        public MethodTranslationInfo TranslationInfo
        {
            get
            {
                return _translationInfo;
            }
            set
            {
                _translationInfo = value;
            }
        }
        private MethodTranslationInfo _translationInfo;
        #endregion Properties

    }
}
