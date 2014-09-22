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
    
    property IsStandardPhpClass bool 
    smartClassEnd
    */
    
    public partial class PhpMethodCallExpression : IPhpValueBase
    {
		#region Constructors 

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name"></param>
        /// </summary>
        public PhpMethodCallExpression(string name, params PhpMethodInvokeValue[] args)
        {
            _name = name;
            _arguments.AddRange(args);
        }

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

        public static PhpMethodCallExpression MakeConstructor(string constructedClassName, params IPhpValue[] args)
        {
            var methodCallExpression = new PhpMethodCallExpression(ConstructorMethodName, args)
            {
                ClassName = (PhpQualifiedName)constructedClassName
            };            
            return methodCallExpression;
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var requests = IPhpStatementBase.GetCodeRequests(_arguments.Select(i => i.Expression)).ToList();
            if (!_className.IsEmpty && !_isStandardPhpClass)
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


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 09:38
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
        implement ToString ##Name## ##Arguments## ##IsConstructorCall## ##TargetObject## ##ClassName## ##IsStandardPhpClass##
        implement ToString Name=##Name##, Arguments=##Arguments##, IsConstructorCall=##IsConstructorCall##, TargetObject=##TargetObject##, ClassName=##ClassName##, IsStandardPhpClass=##IsStandardPhpClass##
        implement equals Name, Arguments, IsConstructorCall, TargetObject, ClassName, IsStandardPhpClass
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
        /// Nazwa własności IsStandardPhpClass; 
        /// </summary>
        public const string PropertyNameIsStandardPhpClass = "IsStandardPhpClass";
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
        /// Nazwa klasy dla konstruktora lub metody statycznej
        /// </summary>
        public PhpQualifiedName ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                _className = value;
            }
        }
        private PhpQualifiedName _className;
        /// <summary>
        /// 
        /// </summary>
        public bool IsStandardPhpClass
        {
            get
            {
                return _isStandardPhpClass;
            }
            set
            {
                _isStandardPhpClass = value;
            }
        }
        private bool _isStandardPhpClass;
        #endregion Properties
    }
}
