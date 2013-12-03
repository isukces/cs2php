using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAttribute : Attribute
    {
        public PageAttribute()
        {

        }
        public PageAttribute(string ModuleShortName)
        {
            this.ModuleShortName = ModuleShortName;
        }
        public string ModuleShortName { get; set; }
    }
}
