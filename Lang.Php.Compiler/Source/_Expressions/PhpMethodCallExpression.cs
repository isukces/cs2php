using Lang.Php.Compiler.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    	read only name == "*"
    
    property TargetObject IPhpValue 
    
    property ClassName PhpQualifiedName Nazwa klasy dla konstruktora lub metody statycznej
    
    property IsStandardPhpClass bool 
    smartClassEnd
    */

    public partial class PhpMethodCallExpression : IPhpValueBase
    {
        public static PhpMethodCallExpression MakeConstructor(string constructedClassName, params IPhpValue[] args)
        {
            PhpMethodCallExpression a = new PhpMethodCallExpression("*", args);
            a.ClassName = constructedClassName;
            return a;
        }

        #region Constructors

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name"></param>
        /// </summary>
        public PhpMethodCallExpression(string Name, params PhpMethodInvokeValue[] args)
        {
            this.Name = Name;
            this.arguments.AddRange(args);
        }



        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name"></param>
        /// </summary>
        public PhpMethodCallExpression(string Name, params IPhpValue[] args)
        {

            this.Name = Name;
            this.arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name"></param>
        /// </summary>
        public PhpMethodCallExpression(IPhpValue TargetObject, string Name, params IPhpValue[] args)
        {
            this.Name = Name;
            this.arguments.AddRange(args.Select(i => new PhpMethodInvokeValue(i)));
            this.targetObject = TargetObject;
        }


        #endregion Constructors

        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var g = IPhpStatementBase.XXX<IPhpValue>(arguments.Select(i => i.Expression)).ToList();
            if (className != null && !isStandardPhpClass)
                g.Add(new ClassCodeRequest(className));
            return g;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            string join = style == null || style.Compression == EmitStyleCompression.Beauty ? ", " : ",";
            var xstyle = PhpEmitStyle.xClone(style);
            var arguments_ = string.Join(join, arguments.Select(i => i.GetPhpCode(xstyle)));
            if (IsConstructorCall)
            {
                var a = string.Format("new {0}({1})", className.NameForEmit(style), arguments_);
                return a;
            }
            else
            {
                var _name = name;
                if (!PhpQualifiedName.IsEmpty(className))
                    _name = className.NameForEmit(style) + "::" + _name;
                else if (targetObject != null)
                {
                    var to = targetObject;
                    if (targetObject is PhpMethodCallExpression && (targetObject as PhpMethodCallExpression).IsConstructorCall)
                        to = new PhpParenthesizedExpression(to);
                    _name = to.GetPhpCode(style) + "->" + _name;
                }
                string a;
                if (_name == "echo")
                    a = string.Format("{0} {1}", _name, arguments_);
                else
                    a = string.Format("{0}({1})", _name, arguments_);
                return a;
            }
        }



        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-03 09:09
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="Name"></param>
        /// </summary>
        public PhpMethodCallExpression(string Name)
        {
            this.Name = Name;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; 
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Arguments; 
        /// </summary>
        public const string PROPERTYNAME_ARGUMENTS = "Arguments";
        /// <summary>
        /// Nazwa własności IsConstructorCall; 
        /// </summary>
        public const string PROPERTYNAME_ISCONSTRUCTORCALL = "IsConstructorCall";
        /// <summary>
        /// Nazwa własności TargetObject; 
        /// </summary>
        public const string PROPERTYNAME_TARGETOBJECT = "TargetObject";
        /// <summary>
        /// Nazwa własności ClassName; Nazwa klasy dla konstruktora lub metody statycznej
        /// </summary>
        public const string PROPERTYNAME_CLASSNAME = "ClassName";
        /// <summary>
        /// Nazwa własności IsStandardPhpClass; 
        /// </summary>
        public const string PROPERTYNAME_ISSTANDARDPHPCLASS = "IsStandardPhpClass";
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
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                name = value;
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpMethodInvokeValue> Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }
        private List<PhpMethodInvokeValue> arguments = new List<PhpMethodInvokeValue>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsConstructorCall
        {
            get
            {
                return name == "*";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue TargetObject
        {
            get
            {
                return targetObject;
            }
            set
            {
                targetObject = value;
            }
        }
        private IPhpValue targetObject;
        /// <summary>
        /// Nazwa klasy dla konstruktora lub metody statycznej
        /// </summary>
        public PhpQualifiedName ClassName
        {
            get
            {
                return className;
            }
            set
            {
                className = value;
            }
        }
        private PhpQualifiedName className;
        /// <summary>
        /// 
        /// </summary>
        public bool IsStandardPhpClass
        {
            get
            {
                return isStandardPhpClass;
            }
            set
            {
                isStandardPhpClass = value;
            }
        }
        private bool isStandardPhpClass;
        #endregion Properties

    }
}
