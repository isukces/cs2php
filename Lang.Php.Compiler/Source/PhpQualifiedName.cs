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
    implement ToString PhpClassName = ##EmitName##
    implement ICloneable 
    implement Equals Fullname
    
    property FullName string pełna nazwa
    
    property Namespace PhpNamespace Przestrzeń nazw
    	read only getNamespace()
    
    property ShortName string nazwa krótka
    	read only getShortName()
    
    property CurrentEffectiveName string nazwa dostępna w obecnym kontekście, np. self
    
    property EmitName string 
    	access private private private
    	read only string.IsNullOrEmpty(CurrentEffectiveName) ? fullName : currentEffectiveName
    smartClassEnd
    */

    public partial class PhpQualifiedName
    {
		#region Static Methods 

		// Public Methods 

        public static bool IsEmpty(PhpQualifiedName x)
        {
            return x == null || string.IsNullOrEmpty(x.fullName);
        }

        public static implicit operator PhpQualifiedName(string x)
        {
            if (x == null)
                return null;
            return new PhpQualifiedName() { FullName = x };
        }

        /// <summary>
        /// Not yet finished
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string SanitizePhpName(string n)
        {
            n = n.Trim();
            var nl = n.ToLower();
            if (nl == "namespace")
                return "_" + n;
            return n;
        }

        public static implicit operator string(PhpQualifiedName x)
        {
            if (x == null)
                return null;
            return x.fullName;
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public string GetNameRelatedTo(PhpQualifiedName other)
        {
            if (other.fullName == fullName)
                return SELF;
#warning 'Jeśli będą przestrzenie nazw to trzeba to rozbudować'
            return fullName;
        }

        /// <summary>
        /// Tworzy nazwę absolutną (bez skrótów typu self lub parent)
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public PhpQualifiedName MakeAbsolute()
        {
            var clone = XClone();
            clone.CurrentEffectiveName = "";
            return clone;
        }

        public string NameForEmit(PhpEmitStyle style)
        {
            if (style == null)
                return EmitName;
            if (this == style.CurrentClass)
                return SELF;
            if (style.CurrentNamespace == null)
                return fullName;
            {
                if ((fullName + T_NS_SEPARATOR).StartsWith(style.CurrentNamespace.Name + T_NS_SEPARATOR))
                    return fullName.Substring(style.CurrentNamespace.Name.Length + 1);
            }
            if (style.CurrentNamespace == Namespace)
                return ShortName;
            if (style.CurrentNamespace != null)
            {
                if (Namespace == null && !fullName.StartsWith(T_NS_SEPARATOR.ToString()))
                    return T_NS_SEPARATOR + fullName;
            }

            return EmitName;
        }

        public void SetEffectiveNameRelatedTo(PhpQualifiedName other)
        {
            var effectiveNameCandidate = GetNameRelatedTo(other);
            CurrentEffectiveName = effectiveNameCandidate != fullName ? effectiveNameCandidate : "";
        }

        public PhpQualifiedName XClone()
        {
            return (PhpQualifiedName)((this as ICloneable).Clone());
        }
		// Private Methods 

        PhpNamespace getNamespace()
        {
            var a = fullName.LastIndexOf(T_NS_SEPARATOR);
            if (a < 0) return PhpNamespace.Root;
            return (PhpNamespace)fullName.Substring(0, a);
        }

        string getShortName()
        {
            var a = fullName.LastIndexOf(T_NS_SEPARATOR);
            if (a < 0) return fullName;
            return fullName.Substring(a + 1);
        }

		#endregion Methods 

		#region Fields 

        public const string PARENT = "parent";
        public const string SELF = "self";
        public const char T_NS_SEPARATOR = '\\';

		#endregion Fields 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-03 12:58
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpQualifiedName : IEquatable<PhpQualifiedName>, ICloneable
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpQualifiedName()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##FullName## ##Namespace## ##ShortName## ##CurrentEffectiveName## ##EmitName##
        implement ToString FullName=##FullName##, Namespace=##Namespace##, ShortName=##ShortName##, CurrentEffectiveName=##CurrentEffectiveName##, EmitName=##EmitName##
        implement equals FullName, Namespace, ShortName, CurrentEffectiveName, EmitName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region ICloneable
        /// <summary>
        /// Creates copy of object
        /// </summary>
        object ICloneable.Clone()
        {
            PhpQualifiedName myClone = new PhpQualifiedName();
            myClone.FullName = this.FullName;
            myClone.CurrentEffectiveName = this.CurrentEffectiveName;
            return myClone;
        }

        #endregion ICloneable

        #region Constants
        /// <summary>
        /// Nazwa własności FullName; pełna nazwa
        /// </summary>
        public const string PROPERTYNAME_FULLNAME = "FullName";
        /// <summary>
        /// Nazwa własności Namespace; Przestrzeń nazw
        /// </summary>
        public const string PROPERTYNAME_NAMESPACE = "Namespace";
        /// <summary>
        /// Nazwa własności ShortName; nazwa krótka
        /// </summary>
        public const string PROPERTYNAME_SHORTNAME = "ShortName";
        /// <summary>
        /// Nazwa własności CurrentEffectiveName; nazwa dostępna w obecnym kontekście, np. self
        /// </summary>
        public const string PROPERTYNAME_CURRENTEFFECTIVENAME = "CurrentEffectiveName";
        /// <summary>
        /// Nazwa własności EmitName; 
        /// </summary>
        public const string PROPERTYNAME_EMITNAME = "EmitName";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("PhpClassName = {0}", EmitName);
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpQualifiedName other)
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
            if (!(other is PhpQualifiedName)) return false;
            return Equals((PhpQualifiedName)other);
        }

        /// <summary>
        /// Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            return fullName.GetHashCode();
        }

        #endregion Methods

        #region Operators
        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(PhpQualifiedName left, PhpQualifiedName right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.fullName == right.fullName;
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(PhpQualifiedName left, PhpQualifiedName right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.fullName != right.fullName;
        }

        #endregion Operators

        #region Properties
        /// <summary>
        /// pełna nazwa
        /// </summary>
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                fullName = value;
            }
        }
        private string fullName = string.Empty;
        /// <summary>
        /// Przestrzeń nazw; własność jest tylko do odczytu.
        /// </summary>
        public PhpNamespace Namespace
        {
            get
            {
                return getNamespace();
            }
        }
        /// <summary>
        /// nazwa krótka; własność jest tylko do odczytu.
        /// </summary>
        public string ShortName
        {
            get
            {
                return getShortName();
            }
        }
        /// <summary>
        /// nazwa dostępna w obecnym kontekście, np. self
        /// </summary>
        public string CurrentEffectiveName
        {
            get
            {
                return currentEffectiveName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                currentEffectiveName = value;
            }
        }
        private string currentEffectiveName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        private string EmitName
        {
            get
            {
                return string.IsNullOrEmpty(CurrentEffectiveName) ? fullName : currentEffectiveName;
            }
        }
        #endregion Properties

    }
}
