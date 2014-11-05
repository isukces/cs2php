using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ResourcesDirectoryAttribute : Attribute
    {
        #region Constructors

        public ResourcesDirectoryAttribute(string Source)
        {
            this.Source = Source;
            Destination = "";
        }

        public ResourcesDirectoryAttribute(string Source, string Destination)
        {
            this.Source = Source;
            this.Destination = Destination;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Where to place resources
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        /// Where to search resources
        /// </summary>
        public string Source { get; private set; }

        #endregion Properties
    }
}
