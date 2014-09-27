using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Method, TargetObject, GPMethod
    
    property Method MethodInfo 
    
    property GPMethod MethodInfo 
    
    property TargetObject object 
    
    property Priority int? 
    	read only GetPriority()
    smartClassEnd
    */
    
    /// <summary>
    /// Zawiera opakowanie dla jednego translatora i potrafi go wywołać
    /// </summary>
    public partial class NodeTranslatorBound
    {
        public IPhpValue Translate(IExternalTranslationContext ctx, object node)
        {
            return _method.Invoke(_targetObject, new[] { ctx, node }) as IPhpValue;
        }

        public override string ToString()
        {
            return string.Format("NodeTranslatorBound {0} {1} for {2}", 
                Priority, 
                TargetObject.GetType().ExcName(),
                _method.GetParameters()[1].ParameterType.ExcName());
        }

        int GetPriority()
        {
            if (!priority.HasValue)
            {
                priority = (int)_gPMethod.Invoke(_targetObject, new object[0]);
            }
            return priority.Value;
            //   return method.Invoke(targetObject, new object[] { ctx, node }) as IPhpValue;
        }
        int? priority;
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-17 14:16
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class NodeTranslatorBound 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public NodeTranslatorBound()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Method## ##GPMethod## ##TargetObject## ##Priority##
        implement ToString Method=##Method##, GPMethod=##GPMethod##, TargetObject=##TargetObject##, Priority=##Priority##
        implement equals Method, GPMethod, TargetObject, Priority
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Method"></param>
        /// <param name="TargetObject"></param>
        /// <param name="GPMethod"></param>
        /// </summary>
        public NodeTranslatorBound(MethodInfo Method, object TargetObject, MethodInfo GPMethod)
        {
            this.Method = Method;
            this.TargetObject = TargetObject;
            this.GPMethod = GPMethod;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Method; 
        /// </summary>
        public const string PROPERTYNAME_METHOD = "Method";
        /// <summary>
        /// Nazwa własności GPMethod; 
        /// </summary>
        public const string PROPERTYNAME_GPMETHOD = "GPMethod";
        /// <summary>
        /// Nazwa własności TargetObject; 
        /// </summary>
        public const string PROPERTYNAME_TARGETOBJECT = "TargetObject";
        /// <summary>
        /// Nazwa własności Priority; 
        /// </summary>
        public const string PROPERTYNAME_PRIORITY = "Priority";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return _method;
            }
            set
            {
                _method = value;
            }
        }
        private MethodInfo _method;
        /// <summary>
        /// 
        /// </summary>
        public MethodInfo GPMethod
        {
            get
            {
                return _gPMethod;
            }
            set
            {
                _gPMethod = value;
            }
        }
        private MethodInfo _gPMethod;
        /// <summary>
        /// 
        /// </summary>
        public object TargetObject
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
        private object _targetObject;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public int? Priority
        {
            get
            {
                return GetPriority();
            }
        }
        #endregion Properties

    }
}
