using System;
using System.Linq;
using System.Xml.Linq;

namespace Lang.Cs.Compiler.VSProject
{
    public class ProjectReference
    {
        public static ProjectReference Deserialize(XElement e)
        {
            /*  <ProjectReference Include="..\Lang.Php\Lang.Php.csproj">
       <Project>{ed717576-b7b9-4775-8236-1855e20e52d5}</Project>
       <Name>Lang.Php</Name>
     </ProjectReference>*/
            var ns = e.Name.Namespace;
            var path = (string)e.Attribute("Include");
            var name = e.Element(ns + "Name")?.Value;
            if (string.IsNullOrEmpty(name))
            {
                name = path.Split('/', '\\').Last();
                if (name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                    name = name.Substring(0, name.Length - 7);
            }
            return new ProjectReference
            {
                _path       = path,
                _name       = name
            };
        }

        /// <summary>
        /// </summary>
        public string Path
        {
            get => _path;
            set => _path = (value ?? string.Empty).Trim();
        }

        /*
        /// <summary>
        /// </summary>
        public Guid ProjectGuid { get; set; }
        */

        /// <summary>
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = (value ?? string.Empty).Trim();
        }

        private string _path = string.Empty;
        private string _name = string.Empty;
    }
}