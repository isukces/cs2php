using System;

namespace Lang.Php.Data
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FieldGroupAttribute : Attribute
    {
        public FieldGroupAttribute(string GroupName)
        {
            this.GroupName = GroupName;
        }
        public string GroupName { get; private set; }

        /// <summary>
        /// Suggested field name for autoincrement fieldor property
        /// </summary>
        public const string AUTOINCREMENT = "autoincrement";
        /// <summary>
        /// Suggested field name for immutable field or property
        /// </summary>
        public const string IMMUTABLE = "immutable";
    }
}
