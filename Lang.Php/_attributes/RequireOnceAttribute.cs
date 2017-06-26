using System;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireOnceAttribute : Attribute
    {
        public RequireOnceAttribute(string f)
        {
            Filename = f;
        }
        public string Filename { get; private set; }
    }
}
