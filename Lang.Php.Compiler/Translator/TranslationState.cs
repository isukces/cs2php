using Lang.Cs.Compiler;
using Lang.Cs.Compiler.Sandbox;
using System;
using System.Linq;

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
    
    public sealed partial class TranslationState : IExternalTranslationContext
    {
		#region Methods 

		// Public Methods 

        public ClassReplaceInfo FindOneClassReplacer(Type srcType)
        {
            var type = srcType;
            if (type.IsGenericType)
                type = type.GetGenericTypeDefinition();
            var replacers = ClassReplacers.Where(i => i.SourceType == type).ToArray();
            if (!replacers.Any()) return null;
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
		// Private Methods 

        TranslationInfo IExternalTranslationContext.GetTranslationInfo()
        {
            return _principles;
        }

        IPhpValue IExternalTranslationContext.TranslateValue(IValue srcValue)
        {
            //var aa = new ExpressionSimplifier(new OptimizeOptions());
            //srcValue = aa.Simplify(srcValue);

            var t = new PhpValueTranslator(this);
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
                return _cl ?? (_cl = (from assembly in _principles.TranslationAssemblies
                    from type in assembly.GetTypes()
                    let attributes = type.GetCustomAttributes(false).OfType<ReplaceAttribute>().ToArray()
                    where attributes != null && attributes.Any()
                    from attribute in attributes
                    select new ClassReplaceInfo(attribute.ReplacedType, type)
                    ).Distinct().ToArray());
            }
        }

		#endregion Properties 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 18:12
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="principles"></param>
        /// </summary>
        public TranslationState(TranslationInfo principles)
        {
            _principles = principles;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Principles; 
        /// </summary>
        public const string PropertyNamePrinciples = "Principles";
        /// <summary>
        /// Nazwa własności PhpVersion; 
        /// </summary>
        public const string PropertyNamePhpVersion = "PhpVersion";
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
                return _principles;
            }
        }
        private TranslationInfo _principles;
        /// <summary>
        /// 
        /// </summary>
        public Version PhpVersion
        {
            get
            {
                return _phpVersion;
            }
            set
            {
                _phpVersion = value;
            }
        }
        private Version _phpVersion = new Version(5,3,0);
        #endregion Properties

    }
}
