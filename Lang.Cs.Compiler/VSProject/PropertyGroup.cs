using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    
    property AssemblyName string nazwa DLL lub Exe
    
    property OutputType string 
    
    property ProjectGuid Guid? 
    
    property OutputPath string 
    
    property DefineConstants string 
    
    property DebugSymbols bool 
    smartClassEnd
    */
    
    public partial class PropertyGroup
    {
        #region Static Methods

        // Public Methods 

        public static PropertyGroup Deserialize(XElement e)
        {
            PropertyGroup p = new PropertyGroup();
            var ns = e.Name.Namespace;
            var g = e.Element(ns + "DebugSymbols");
            if (g != null)
                p.DebugSymbols = (bool)g;
            p.DefineConstants = (string)e.Element(ns + "DefineConstants");
            p.OutputPath = (string)e.Element(ns + "OutputPath");

            p.OutputType = (string)e.Element(ns + "OutputType");
            p.AssemblyName = (string)e.Element(ns + "AssemblyName");
            p.ProjectGuid = (Guid?)e.Element(ns + "ProjectGuid");

            return p;
        }

        #endregion Static Methods

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
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 10:25
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class PropertyGroup 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PropertyGroup()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##AssemblyName## ##OutputType## ##ProjectGuid## ##OutputPath## ##DefineConstants## ##DebugSymbols##
        implement ToString AssemblyName=##AssemblyName##, OutputType=##OutputType##, ProjectGuid=##ProjectGuid##, OutputPath=##OutputPath##, DefineConstants=##DefineConstants##, DebugSymbols=##DebugSymbols##
        implement equals AssemblyName, OutputType, ProjectGuid, OutputPath, DefineConstants, DebugSymbols
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności AssemblyName; nazwa DLL lub Exe
        /// </summary>
        public const string PROPERTYNAME_ASSEMBLYNAME = "AssemblyName";
        /// <summary>
        /// Nazwa własności OutputType; 
        /// </summary>
        public const string PROPERTYNAME_OUTPUTTYPE = "OutputType";
        /// <summary>
        /// Nazwa własności ProjectGuid; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTGUID = "ProjectGuid";
        /// <summary>
        /// Nazwa własności OutputPath; 
        /// </summary>
        public const string PROPERTYNAME_OUTPUTPATH = "OutputPath";
        /// <summary>
        /// Nazwa własności DefineConstants; 
        /// </summary>
        public const string PROPERTYNAME_DEFINECONSTANTS = "DefineConstants";
        /// <summary>
        /// Nazwa własności DebugSymbols; 
        /// </summary>
        public const string PROPERTYNAME_DEBUGSYMBOLS = "DebugSymbols";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa DLL lub Exe
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                assemblyName = value;
            }
        }
        private string assemblyName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OutputType
        {
            get
            {
                return outputType;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                outputType = value;
            }
        }
        private string outputType = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Guid? ProjectGuid
        {
            get
            {
                return projectGuid;
            }
            set
            {
                projectGuid = value;
            }
        }
        private Guid? projectGuid;
        /// <summary>
        /// 
        /// </summary>
        public string OutputPath
        {
            get
            {
                return outputPath;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                outputPath = value;
            }
        }
        private string outputPath = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string DefineConstants
        {
            get
            {
                return defineConstants;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                defineConstants = value;
            }
        }
        private string defineConstants = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool DebugSymbols
        {
            get
            {
                return debugSymbols;
            }
            set
            {
                debugSymbols = value;
            }
        }
        private bool debugSymbols;
        #endregion Properties

    }
}
