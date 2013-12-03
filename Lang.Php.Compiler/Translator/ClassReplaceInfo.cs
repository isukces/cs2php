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
    implement Constructor *
    implement ToString ##SourceType##  => ##ReplaceBy##
    implement Equals *
    
    property SourceType Type Typ oryginalny .NET
    	read only
    
    property ReplaceBy Type typ, którym zastępujemy
    	read only
    smartClassEnd
    */
    
    public partial class ClassReplaceInfo
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-04 09:34
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Translator
{
    public partial class ClassReplaceInfo : IEquatable<ClassReplaceInfo>
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ClassReplaceInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##SourceType## ##ReplaceBy##
        implement ToString SourceType=##SourceType##, ReplaceBy=##ReplaceBy##
        implement equals SourceType, ReplaceBy
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="SourceType">Typ oryginalny .NET</param>
        /// <param name="ReplaceBy">typ, którym zastępujemy</param>
        /// </summary>
        public ClassReplaceInfo(Type SourceType, Type ReplaceBy)
        {
            this.sourceType = SourceType;
            this.replaceBy = ReplaceBy;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności SourceType; Typ oryginalny .NET
        /// </summary>
        public const string PROPERTYNAME_SOURCETYPE = "SourceType";
        /// <summary>
        /// Nazwa własności ReplaceBy; typ, którym zastępujemy
        /// </summary>
        public const string PROPERTYNAME_REPLACEBY = "ReplaceBy";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}  => {1}", sourceType, replaceBy);
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(ClassReplaceInfo other)
        {
            return other == this;
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is ClassReplaceInfo)) return false;
            return Equals((ClassReplaceInfo)other);
        }

        /// <summary>
        /// Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            // Good implementation suggested by Josh Bloch
            int _hash_ = 17;
            _hash_ = _hash_ * 31 + sourceType.GetHashCode();
            _hash_ = _hash_ * 31 + replaceBy.GetHashCode();
            return _hash_;
        }

        #endregion Methods

        #region Operators
        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(ClassReplaceInfo left, ClassReplaceInfo right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.sourceType == right.sourceType && left.replaceBy == right.replaceBy;
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(ClassReplaceInfo left, ClassReplaceInfo right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.sourceType != right.sourceType || left.replaceBy != right.replaceBy;
        }

        #endregion Operators

        #region Properties
        /// <summary>
        /// Typ oryginalny .NET; własność jest tylko do odczytu.
        /// </summary>
        public Type SourceType
        {
            get
            {
                return sourceType;
            }
        }
        private Type sourceType;
        /// <summary>
        /// typ, którym zastępujemy; własność jest tylko do odczytu.
        /// </summary>
        public Type ReplaceBy
        {
            get
            {
                return replaceBy;
            }
        }
        private Type replaceBy;
        #endregion Properties

    }
}
