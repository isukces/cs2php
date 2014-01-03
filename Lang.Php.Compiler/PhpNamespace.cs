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
    implement Constructor Name
    implement Equals Name
    
    property Name string 
    	init ROOT_NAME
    	preprocess value = Prepare(value);
    	OnChange isRoot = IsRootNamespace(name);
    
    property IsRoot bool 
    	init true
    	read only
    smartClassEnd
    */

    public partial class PhpNamespace
    {
		#region Static Methods 

		// Public Methods 

        public static bool IsRootNamespace(string name)
        {
            return Prepare(name) == ROOT_NAME;
        }

        public static explicit operator PhpNamespace(string src)
        {
            return new PhpNamespace(src);
        }

        public static string Prepare(string ns)
        {
            ns = ns ?? "";
            ns = PathUtil.MakeWinPath(ns);
            if (!ns.StartsWith(PathUtil.WIN_SEP))
                ns = PathUtil.WIN_SEP + ns;
            return ns;
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public override string ToString()
        {
            return name;
        }

		#endregion Methods 

		#region Fields 

        public const string ROOT_NAME = PathUtil.WIN_SEP;

		#endregion Fields 

		#region Static Properties 

        public static PhpNamespace Root
        {
            get
            {
                return new PhpNamespace(ROOT_NAME);
            }
        }

		#endregion Static Properties 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-03 12:53
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class PhpNamespace : IEquatable<PhpNamespace>
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpNamespace()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##IsRoot##
        implement ToString Name=##Name##, IsRoot=##IsRoot##
        implement equals Name, IsRoot
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name"></param>
        /// </summary>
        public PhpNamespace(string Name)
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
        /// Nazwa własności IsRoot; 
        /// </summary>
        public const string PROPERTYNAME_ISROOT = "IsRoot";
        #endregion Constants


        #region Methods
        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpNamespace other)
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
            if (!(other is PhpNamespace)) return false;
            return Equals((PhpNamespace)other);
        }

        /// <summary>
        /// Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        #endregion Methods


        #region Operators
        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(PhpNamespace left, PhpNamespace right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.name == right.name;
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(PhpNamespace left, PhpNamespace right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.name != right.name;
        }

        #endregion Operators


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
                value = Prepare(value);
                if (value == name) return;
                name = value;
                isRoot = IsRootNamespace(name);
            }
        }
        private string name = ROOT_NAME;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return isRoot;
            }
        }
        private bool isRoot = true;
        #endregion Properties
    }
}
