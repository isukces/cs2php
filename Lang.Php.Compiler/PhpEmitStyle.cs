using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement ICloneable 
    
    property AsIncrementor bool 
    
    property Brackets ShowBracketsEnum 
    
    property Compression EmitStyleCompression 
    
    property UseBracketsEvenIfNotNecessary bool 
    
    property CurrentNamespace string 
    
    property CurrentClass PhpQualifiedName Name of current class
    smartClassEnd
    */
    
    public partial class PhpEmitStyle
    {
        
        public static PhpEmitStyle xClone(PhpEmitStyle x)
        {
            if (x == null)
                return new PhpEmitStyle();
            return (PhpEmitStyle)(x as ICloneable).Clone();
        }
        public static PhpEmitStyle xClone(PhpEmitStyle x, ShowBracketsEnum e)
        {
            var tmp = xClone(x);
            tmp.Brackets = e;
            return tmp;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-03 10:25
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class PhpEmitStyle : ICloneable
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpEmitStyle()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##AsIncrementor## ##Brackets## ##Compression## ##UseBracketsEvenIfNotNecessary## ##CurrentNamespace## ##CurrentClass##
        implement ToString AsIncrementor=##AsIncrementor##, Brackets=##Brackets##, Compression=##Compression##, UseBracketsEvenIfNotNecessary=##UseBracketsEvenIfNotNecessary##, CurrentNamespace=##CurrentNamespace##, CurrentClass=##CurrentClass##
        implement equals AsIncrementor, Brackets, Compression, UseBracketsEvenIfNotNecessary, CurrentNamespace, CurrentClass
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region ICloneable
        /// <summary>
        /// Creates copy of object
        /// </summary>
        object ICloneable.Clone()
        {
            PhpEmitStyle myClone = new PhpEmitStyle();
            myClone.AsIncrementor = this.AsIncrementor;
            myClone.Brackets = this.Brackets;
            myClone.Compression = this.Compression;
            myClone.UseBracketsEvenIfNotNecessary = this.UseBracketsEvenIfNotNecessary;
            myClone.CurrentNamespace = this.CurrentNamespace;
            myClone.CurrentClass = this.CurrentClass;
            return myClone;
        }

        #endregion ICloneable

        #region Constants
        /// <summary>
        /// Nazwa własności AsIncrementor; 
        /// </summary>
        public const string PROPERTYNAME_ASINCREMENTOR = "AsIncrementor";
        /// <summary>
        /// Nazwa własności Brackets; 
        /// </summary>
        public const string PROPERTYNAME_BRACKETS = "Brackets";
        /// <summary>
        /// Nazwa własności Compression; 
        /// </summary>
        public const string PROPERTYNAME_COMPRESSION = "Compression";
        /// <summary>
        /// Nazwa własności UseBracketsEvenIfNotNecessary; 
        /// </summary>
        public const string PROPERTYNAME_USEBRACKETSEVENIFNOTNECESSARY = "UseBracketsEvenIfNotNecessary";
        /// <summary>
        /// Nazwa własności CurrentNamespace; 
        /// </summary>
        public const string PROPERTYNAME_CURRENTNAMESPACE = "CurrentNamespace";
        /// <summary>
        /// Nazwa własności CurrentClass; Name of current class
        /// </summary>
        public const string PROPERTYNAME_CURRENTCLASS = "CurrentClass";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool AsIncrementor
        {
            get
            {
                return asIncrementor;
            }
            set
            {
                asIncrementor = value;
            }
        }
        private bool asIncrementor;
        /// <summary>
        /// 
        /// </summary>
        public ShowBracketsEnum Brackets
        {
            get
            {
                return brackets;
            }
            set
            {
                brackets = value;
            }
        }
        private ShowBracketsEnum brackets;
        /// <summary>
        /// 
        /// </summary>
        public EmitStyleCompression Compression
        {
            get
            {
                return compression;
            }
            set
            {
                compression = value;
            }
        }
        private EmitStyleCompression compression;
        /// <summary>
        /// 
        /// </summary>
        public bool UseBracketsEvenIfNotNecessary
        {
            get
            {
                return useBracketsEvenIfNotNecessary;
            }
            set
            {
                useBracketsEvenIfNotNecessary = value;
            }
        }
        private bool useBracketsEvenIfNotNecessary;
        /// <summary>
        /// 
        /// </summary>
        public string CurrentNamespace
        {
            get
            {
                return currentNamespace;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                currentNamespace = value;
            }
        }
        private string currentNamespace = string.Empty;
        /// <summary>
        /// Name of current class
        /// </summary>
        public PhpQualifiedName CurrentClass
        {
            get
            {
                return currentClass;
            }
            set
            {
                currentClass = value;
            }
        }
        private PhpQualifiedName currentClass;
        #endregion Properties

    }
}
