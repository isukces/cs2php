using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BuiltInAttribute : Attribute
    {
    }
}
