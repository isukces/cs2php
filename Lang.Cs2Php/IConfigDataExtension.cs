namespace Lang.Cs2Php
{
    public static class ConfigDataExtension 
    {
        public static void CopyFrom(this IConfigData x, IConfigData s)
        {
            x.Configuration = s.Configuration;
            x.CsProject = s.CsProject;
            x.OutDir = s.OutDir;
            x.Referenced.Clear(); x.Referenced.AddRange(s.Referenced);
            x.TranlationHelpers.Clear(); x.TranlationHelpers.AddRange(s.TranlationHelpers);
            x.ReferencedPhpLibsLocations.Clear();
            foreach (var a in s.ReferencedPhpLibsLocations)
                x.ReferencedPhpLibsLocations.Add(a.Key, a.Value);
        }
    }
}