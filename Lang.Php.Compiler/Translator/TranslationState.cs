using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Principles
    
    property Principles TranslationInfo 
    	read only
    
    property PhpVersion Version 
    	init new Version(5,3,0)
    smartClassEnd
    */
    
    public partial class TranslationState : IExternalTranslationContext
    {
		#region Methods 

		// Public Methods 

        public ClassReplaceInfo FindOneClassReplacer(Type srcType)
        {
            var type = srcType;
            if (type.IsGenericType)
                type = type.GetGenericTypeDefinition();
            ClassReplaceInfo[] replacers = ClassReplacers.Where(i => i.SourceType == type).ToArray();
            if (replacers.Any())
            {
                if (replacers.Length > 1)
                    throw new Exception(string.Format("wise gecko, class replacers has more than 1 replacer for {0}", type.FullName));
                var atype = replacers[0];

                if (srcType.IsGenericType)
                {
                    var a = srcType.GetGenericArguments();
                    var gtype = atype.ReplaceBy.MakeGenericType(a);
                    atype = new ClassReplaceInfo(srcType, gtype);
                }
                return atype;
            }
            return null;
        }
		// Private Methods 

        TranslationInfo IExternalTranslationContext.GetTranslationInfo()
        {
            return principles;
        }

        IPhpValue IExternalTranslationContext.TranslateValue(Lang.Cs.Compiler.IValue srcValue)
        {
            //var aa = new ExpressionSimplifier(new OptimizeOptions());
            //srcValue = aa.Simplify(srcValue);

            PhpValueTranslator t = new PhpValueTranslator(this);
            var g = t.TransValue(srcValue);
          
            return g;
        }

		#endregion Methods 

		#region Fields 

        ClassReplaceInfo[] _cl;

		#endregion Fields 

		#region Properties 

        public ClassReplaceInfo[] ClassReplacers
        {
            get
            {
                if (_cl == null)
                {
                    _cl = (from _assembly in principles.TranslationAssemblies
                           from _type in _assembly.GetTypes()
                           let _attributes = _type.GetCustomAttributes(false).OfType<ReplaceAttribute>().ToArray()
                           where _attributes != null && _attributes.Any()
                           from _attribute in _attributes
                           select new ClassReplaceInfo(_attribute.ReplacedType, _type)
                                  ).Distinct().ToArray();
                }
                return _cl;
            }
        }

		#endregion Properties 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-08 18:52
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Translator
{
    public partial class TranslationState 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public TranslationState()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Principles## ##PhpVersion##
        implement ToString Principles=##Principles##, PhpVersion=##PhpVersion##
        implement equals Principles, PhpVersion
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Principles"></param>
        /// </summary>
        public TranslationState(TranslationInfo Principles)
        {
            this.principles = Principles;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Principles; 
        /// </summary>
        public const string PROPERTYNAME_PRINCIPLES = "Principles";
        /// <summary>
        /// Nazwa własności PhpVersion; 
        /// </summary>
        public const string PROPERTYNAME_PHPVERSION = "PhpVersion";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public TranslationInfo Principles
        {
            get
            {
                return principles;
            }
        }
        private TranslationInfo principles;
        /// <summary>
        /// 
        /// </summary>
        public Version PhpVersion
        {
            get
            {
                return phpVersion;
            }
            set
            {
                phpVersion = value;
            }
        }
        private Version phpVersion = new Version(5,4,0);
        #endregion Properties

    }
}
