using Lang.Php.Compiler.Source;


namespace Lang.Php.Compiler
{
    public class ClassCodeRequest : ICodeRequest
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="className"></param>
        /// </summary>
        public ClassCodeRequest(PhpQualifiedName className)
        {
            ClassName = className;
        }


        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return "ClassCodeRequest ##PhpClassName##";
        }

        /// <summary>
        /// </summary>
        public PhpQualifiedName ClassName { get; set; }
    }
}