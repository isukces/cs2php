using System;
using System.Globalization;

namespace Lang.Php.Compiler.Source
{  
    public struct PhpQualifiedName : IEquatable<PhpQualifiedName>
    {
        private string _forceName;
        public const string ClassnameParent = "parent";
        public const string ClassnameSelf = "self";
        public const char TokenNsSeparator = '\\';

        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string EmitName
        {
            get
            {
                return string.IsNullOrEmpty(_forceName) ? FullName : _forceName;
            }
        }

        public static PhpQualifiedName Empty
        {
            get
            {
                return new PhpQualifiedName(null);
            }
        }

        /// <summary>
        /// nazwa dostępna w obecnym kontekście, np. self
        /// </summary>
        public string ForceName
        {
            get
            {
                return _forceName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _forceName = value;
            }
        }

        /// <summary>
        /// pełna nazwa
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Przestrzeń nazw; własność jest tylko do odczytu.
        /// </summary>
        public PhpNamespace Namespace
        {
            get
            {
                return GetNamespace();
            }
        }

        /// <summary>
        /// nazwa krótka; własność jest tylko do odczytu.
        /// </summary>
        public string ShortName
        {
            get
            {
                return GetShortName();
            }
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(PhpQualifiedName left, PhpQualifiedName right)
        {
            return left.FullName != right.FullName;
        }

        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(PhpQualifiedName left, PhpQualifiedName right)
        {
            return left.FullName == right.FullName;
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpQualifiedName other)
        {
            return other == this;
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
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
            return (FullName ?? "").GetHashCode();
        }

        private string GetNameRelatedTo(PhpQualifiedName other)
        {
            return other.FullName == FullName ? ClassnameSelf : FullName;
        }

        PhpNamespace GetNamespace()
        {
            var a = FullName.LastIndexOf(TokenNsSeparator);
            if (a < 0) return PhpNamespace.Root;
            return (PhpNamespace)FullName.Substring(0, a);
        }

        string GetShortName()
        {
            var a = FullName.LastIndexOf(TokenNsSeparator);
            return a < 0 ? FullName : FullName.Substring(a + 1);
        }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(FullName); }
        }

        /// <summary>
        /// Tworzy nazwę absolutną (bez skrótów typu self lub parent)
        /// </summary>
        /// <returns></returns>
        public PhpQualifiedName MakeAbsolute()
        {
            var clone = this;
            clone.ForceName = "";
            return clone;
        }

        public string NameForEmit(PhpEmitStyle style)
        {
            if (style == null)
                return EmitName;
            if (this == style.CurrentClass)
                return ClassnameSelf;
            if (style.CurrentNamespace == null)
                return FullName;
            {
                if ((FullName + TokenNsSeparator).StartsWith(style.CurrentNamespace.Name + TokenNsSeparator))
                    return FullName.Substring(style.CurrentNamespace.Name.Length + 1);
            }
            if (style.CurrentNamespace == Namespace)
                return ShortName;
            if (style.CurrentNamespace == null)
                return EmitName;
            if (Namespace == null && !FullName.StartsWith(TokenNsSeparator.ToString(CultureInfo.InvariantCulture)))
                return TokenNsSeparator + FullName;

            return EmitName;
        }

        private PhpQualifiedName(string fullName)
            : this()
        {
            if (fullName != null)
                fullName = fullName.Trim();
            if (fullName == "")
                fullName = null;
            FullName = fullName;
        }

        public static explicit operator PhpQualifiedName(string fullName)
        {
            return new PhpQualifiedName(fullName);
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

        public void SetEffectiveNameRelatedTo(PhpQualifiedName other)
        {
            var effectiveNameCandidate = GetNameRelatedTo(other);
            ForceName = effectiveNameCandidate != FullName ? effectiveNameCandidate : "";
        } 

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("PhpClassName = {0}", EmitName);
        }
    }
}
