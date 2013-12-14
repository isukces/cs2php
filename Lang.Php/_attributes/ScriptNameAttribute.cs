using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lang.Php
{

    /// <summary>
    /// ScriptNameAttribute is used to decorate classes or methods with an alternate name optionally including namespace that should be presented in PHP code. 
    /// <see cref="https://github.com/isukces/cs2php/wiki/ScriptNameAttribute">Wiki</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ScriptNameAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Creates instance of attribute
        /// </summary>
        public ScriptNameAttribute()
        {

        }

        /// <summary>
        /// Creates instance of attribute
        /// </summary>
        /// <param name="name">Name in script</param>
        public ScriptNameAttribute(string name)
        {
            this.Name = name;
        }

        #endregion Constructors

        #region Enums

        public enum Kinds
        {
            Identifier,
            IntIndex
        }

        #endregion Enums

        #region Properties

        public Kinds Kind
        {
            get
            {
                var m = Regex.Match(Name, "^-?\\d+$");
                if (m.Success)
                    return Kinds.IntIndex;
                return Kinds.Identifier;
            }
        }

        /// <summary>
        /// Name in script
        /// </summary>
        public string Name { get; private set; }

        #endregion Properties
    }
}
