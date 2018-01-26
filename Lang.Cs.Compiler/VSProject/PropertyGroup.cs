using System;
using System.Xml.Linq;

namespace Lang.Cs.Compiler.VSProject
{
    public class PropertyGroup
    {
        // Public Methods 

        public static PropertyGroup Deserialize(XElement e)
        {
            var p  = new PropertyGroup();
            var ns = e.Name.Namespace;
            var g  = e.Element(ns + "DebugSymbols");
            if (g != null)
                p.DebugSymbols = (bool)g;
            p.DefineConstants  = (string)e.Element(ns + "DefineConstants");
            p.OutputPath       = (string)e.Element(ns + "OutputPath");

            p.OutputType   = (string)e.Element(ns + "OutputType");
            p.AssemblyName = (string)e.Element(ns + "AssemblyName");
            p.ProjectGuid  = (Guid?)e.Element(ns + "ProjectGuid");

            return p;
        }

        /*
         *  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
      <DebugSymbols>true</DebugSymbols>
      <DebugType>full</DebugType>
      <Optimize>false</Optimize>
      <OutputPath>bin\Debug\</OutputPath>
      <DefineConstants>DEBUG;TRACE</DefineConstants>
      <ErrorReport>prompt</ErrorReport>
      <WarningLevel>4</WarningLevel>
    </PropertyGroup> */

        /// <summary>
        ///     nazwa DLL lub Exe
        /// </summary>
        public string AssemblyName
        {
            get => _assemblyName;
            set => _assemblyName = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public string OutputType
        {
            get => _outputType;
            set
            {
                value       = (value ?? string.Empty).Trim();
                _outputType = value;
            }
        }

        /// <summary>
        /// </summary>
        public Guid? ProjectGuid { get; set; }

        /// <summary>
        /// </summary>
        public string OutputPath
        {
            get => _outputPath;
            set => _outputPath = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public string DefineConstants
        {
            get => _defineConstants;
            set => _defineConstants = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public bool DebugSymbols { get; set; }

        private string _assemblyName    = string.Empty;
        private string _outputType      = string.Empty;
        private string _outputPath      = string.Empty;
        private string _defineConstants = string.Empty;
    }
}