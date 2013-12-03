using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireOnceAttribute : Attribute
    {
        public RequireOnceAttribute(string f)
        {
            this.Filename = f;
        }
        public string Filename { get; private set; }
    }
}
