using System;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class PhpEmitStyle : ICloneable
    {
        public static PhpEmitStyle xClone(PhpEmitStyle x)
        {
            if (x == null)
                return new PhpEmitStyle();
            return (PhpEmitStyle)(x as ICloneable).Clone();
        }

        public static PhpEmitStyle xClone(PhpEmitStyle x, ShowBracketsEnum e)
        {
            var tmp      = xClone(x);
            tmp.Brackets = e;
            return tmp;
        }


        /// <summary>
        ///     Creates copy of object
        /// </summary>
        object ICloneable.Clone()
        {
            var myClone = new PhpEmitStyle
            {
                AsIncrementor                 = AsIncrementor,
                Brackets                      = Brackets,
                Compression                   = Compression,
                UseBracketsEvenIfNotNecessary = UseBracketsEvenIfNotNecessary,
                CurrentNamespace              = CurrentNamespace,
                CurrentClass                  = CurrentClass
            };
            return myClone;
        }

        /// <summary>
        /// </summary>
        public bool AsIncrementor { get; set; }

        /// <summary>
        /// </summary>
        public ShowBracketsEnum Brackets { get; set; }

        /// <summary>
        /// </summary>
        public EmitStyleCompression Compression { get; set; }

        /// <summary>
        /// </summary>
        public bool UseBracketsEvenIfNotNecessary { get; set; }

        /// <summary>
        /// </summary>
        public PhpNamespace CurrentNamespace { get; set; }

        /// <summary>
        ///     Name of current class
        /// </summary>
        public PhpQualifiedName CurrentClass { get; set; }
    }
}