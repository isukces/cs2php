using System;
using System.Diagnostics;
using System.Linq;

namespace Lang.Cs2Php
{
    public static class ConfigDataExtension
    {
        public static void CopyFrom(this CompilerEngine dst, IConfigData src)
        {

            dst.Configuration = src.Configuration;
            dst.CsProject = src.CsProject;
            dst.OutDir = src.OutDir;
            dst.Referenced.Clear();
            dst.TranlationHelpers.Clear();
            dst.ReferencedPhpLibsLocations.Clear();

            // src and dest can be in different application domain
            // we need to add item by item
            foreach (var q in src.Referenced.ToArray())
                dst.Referenced.Add(q);
            foreach (var q in src.TranlationHelpers.ToArray())
                dst.TranlationHelpers.Add(q);
            foreach (var a in src.ReferencedPhpLibsLocations)
                dst.ReferencedPhpLibsLocations.Add(a.Key, a.Value);

            dst.BinaryOutputDir = src.BinaryOutputDir;
            Debug.Assert(dst.Referenced.Count == src.Referenced.Count);
            Debug.Assert(dst.TranlationHelpers.Count == src.TranlationHelpers.Count);
            Debug.Assert(dst.ReferencedPhpLibsLocations.Count == src.ReferencedPhpLibsLocations.Count);
        }
    }
}