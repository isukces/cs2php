using System;

namespace Lang.Php
{
    public class AsDefinedConstAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        /// </summary>
        public AsDefinedConstAttribute()
        {
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="definedConstName"></param>
        /// </summary>
        public AsDefinedConstAttribute(string definedConstName)
        {
            DefinedConstName = definedConstName;
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string DefinedConstName { get; } = string.Empty;
    }
}