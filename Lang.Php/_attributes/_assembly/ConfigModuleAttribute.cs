using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{

    /// <summary>
    /// ConfigModuleAttribute decorates assembly with name of module thath contains cs2php related configuration.
    /// Module name 'cs2php' (filename cs2php.php) is taken if ConfigModuleAttribute is ommited.
    /// </summary>
    /// <remarks>
    /// Cs2php config module contains i.e. definition of global variables or defined constants with path(s) to other module(s).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ConfigModuleAttribute : Attribute
    {
        /// <summary>
        /// Creates instance of attribute
        /// </summary>
        /// <param name="name">Name of module that contains cs2php related configuration</param>
        public ConfigModuleAttribute(string name)
        {
            Name = name;
        }
        public const string DEFAULT = "cs2php";
        /// <summary>
        /// Name of module that contains cs2php related configuration
        /// </summary>
        public string Name { get; private set; }
    }
}
