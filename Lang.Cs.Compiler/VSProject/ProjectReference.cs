using System;
using System.Xml.Linq;

namespace Lang.Cs.Compiler.VSProject
{
    /*
    smartClass
    option NoAdditionalFile
    
    property Path string 
    
    property ProjectGuid Guid 
    
    property Name string 
    smartClassEnd
    */
    
    public partial class ProjectReference
    {
        public static ProjectReference Deserialize(XElement e)
        {
            /*  <ProjectReference Include="..\Lang.Php\Lang.Php.csproj">
       <Project>{ed717576-b7b9-4775-8236-1855e20e52d5}</Project>
       <Name>Lang.Php</Name>
     </ProjectReference>*/
            var ns = e.Name.Namespace;
            return new ProjectReference()
            {
                path = (string)e.Attribute("Include"),
                projectGuid = Guid.Parse(e.Element(ns + "Project").Value),
                name = e.Element(ns + "Name").Value
            };
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 12:41
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class ProjectReference 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ProjectReference()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Path## ##ProjectGuid## ##Name##
        implement ToString Path=##Path##, ProjectGuid=##ProjectGuid##, Name=##Name##
        implement equals Path, ProjectGuid, Name
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Path; 
        /// </summary>
        public const string PROPERTYNAME_PATH = "Path";
        /// <summary>
        /// Nazwa własności ProjectGuid; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTGUID = "ProjectGuid";
        /// <summary>
        /// Nazwa własności Name; 
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                path = value;
            }
        }
        private string path = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Guid ProjectGuid
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
        private Guid projectGuid;
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                name = value;
            }
        }
        private string name = string.Empty;
        #endregion Properties

    }
}
