using System;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReplaceAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="replacedType"></param>
        /// </summary>
        public ReplaceAttribute(Type replacedType)
        {
            ReplacedType = replacedType;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public Type ReplacedType { get; }
    }
}