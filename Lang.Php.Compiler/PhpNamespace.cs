namespace Lang.Php.Compiler
{
    public class PhpNamespace
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Name"></param>
        /// </summary>
        public PhpNamespace(string Name)
        {
            this.Name = Name;
        }
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

        // Public Methods 

        public override string ToString()
        {
            return name;
        }

        public static PhpNamespace Root => new PhpNamespace(ROOT_NAME);

        public const string ROOT_NAME = PathUtil.WIN_SEP;


        #region Constants

        /// <summary>
        ///     Nazwa własności Name;
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";

        /// <summary>
        ///     Nazwa własności IsRoot;
        /// </summary>
        public const string PROPERTYNAME_ISROOT = "IsRoot";

        #endregion Constants


        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpNamespace other)
        {
            return other == this;
        }

        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is PhpNamespace)) return false;
            return Equals((PhpNamespace)other);
        }

        /// <summary>
        ///     Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }


        /// <summary>
        ///     Realizuje operator ==
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
        ///     Realizuje operator !=
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


        /// <summary>
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                value = (value ?? string.Empty).Trim();
                value = Prepare(value);
                if (value == name) return;
                name   = value;
                IsRoot = IsRootNamespace(name);
            }
        }

        private string name = ROOT_NAME;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public bool IsRoot { get; private set; } = true;
    }
}