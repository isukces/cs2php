using System;

namespace Lang.Php.Compiler.Translator
{
    public class ClassReplaceInfo : IEquatable<ClassReplaceInfo>
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="sourceType">Typ oryginalny .NET</param>
        ///     <param name="replaceBy">typ, którym zastępujemy</param>
        /// </summary>
        public ClassReplaceInfo(Type sourceType, Type replaceBy)
        {
            SourceType = sourceType;
            ReplaceBy  = replaceBy;
        }


        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}  => {1}", SourceType, ReplaceBy);
        }

        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(ClassReplaceInfo other)
        {
            return other == this;
        }

        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is ClassReplaceInfo)) return false;
            return Equals((ClassReplaceInfo)other);
        }

        /// <summary>
        ///     Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            // Good implementation suggested by Josh Bloch
            var _hash_ = 17;
            _hash_     = _hash_ * 31 + SourceType.GetHashCode();
            _hash_     = _hash_ * 31 + ReplaceBy.GetHashCode();
            return _hash_;
        }

        /// <summary>
        ///     Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(ClassReplaceInfo left, ClassReplaceInfo right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.SourceType == right.SourceType && left.ReplaceBy == right.ReplaceBy;
        }

        /// <summary>
        ///     Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(ClassReplaceInfo left, ClassReplaceInfo right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.SourceType != right.SourceType || left.ReplaceBy != right.ReplaceBy;
        }

        /// <summary>
        ///     Typ oryginalny .NET; własność jest tylko do odczytu.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        ///     typ, którym zastępujemy; własność jest tylko do odczytu.
        /// </summary>
        public Type ReplaceBy { get; }
    }
}