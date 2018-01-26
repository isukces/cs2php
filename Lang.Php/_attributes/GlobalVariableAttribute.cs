using System;

namespace Lang.Php
{
    public class GlobalVariableAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        /// </summary>
        public GlobalVariableAttribute()
        {
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="globalVariableName"></param>
        /// </summary>
        public GlobalVariableAttribute(string globalVariableName)
        {
            GlobalVariableName = globalVariableName;
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string GlobalVariableName { get; } = string.Empty;
    }
}