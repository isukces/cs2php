using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Lang.Cs2Php
{
    internal class ConfigData : MarshalByRefObject, IConfigData
    {
        #region Constructors

        public ConfigData()
        {
            Referenced = new List<string>();
            TranlationHelpers = new List<string>();
            ReferencedPhpLibsLocations = new Dictionary<string, string>();
        }

        #endregion Constructors

        #region Properties

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Configuration { get; set; }

        public List<string> Referenced { get; private set; }

        public Dictionary<string, string> ReferencedPhpLibsLocations { get; set; }

        public List<string> TranlationHelpers { get; private set; }
        public string CsProject { get; set; }
        public string OutDir { get; set; }
        public string BinaryOutputDir { get; set; }

        #endregion Properties
    }
}