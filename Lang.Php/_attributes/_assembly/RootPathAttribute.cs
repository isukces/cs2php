using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    /// <summary>
    /// RootPathAttribute is used to decorate assembly with default path where assembly 
    /// output (php files and resources) will be stored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class RootPathAttribute : Attribute
    {
        public RootPathAttribute(string path)
        {
            this.Path = path;
        }
        public string Path { get; private set; }
    }
}
