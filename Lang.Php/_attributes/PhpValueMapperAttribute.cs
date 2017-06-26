using System;

namespace Lang.Php
{
    /// <summary>
    /// Allows PHP compiler to find class that implements IMethodMapper and can be used for PHP values mapping
    /// Class cannot be abstract
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PhpValueMapperAttribute : Attribute
    {
    }
}
