using System;
using System.Collections.Generic;

namespace Lang.Cs2Php
{
    public interface IConfigData
    {
        string Configuration { get; set; }
        string CsProject { get; set; }
        string OutDir { get; set; }
        List<string> Referenced { get; }
        Dictionary<string, string> ReferencedPhpLibsLocations { get; set; }
        List<string> TranlationHelpers { get; }
    }
}
