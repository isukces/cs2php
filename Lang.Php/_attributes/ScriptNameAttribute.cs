using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class ScriptNameAttribute : Attribute
    {
        public ScriptNameAttribute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name in script</param>
        public ScriptNameAttribute(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// Name in script
        /// </summary>
        public string Name { get; set; }
    }
}
