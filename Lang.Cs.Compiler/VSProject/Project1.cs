using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Lang.Cs.Compiler.VSProject
{
    public class Project1
    {
        // Public Methods 

        public static Project1 Load(string filename)
        {
            var project = new Project1
            {
                WorkingDirectory = new FileInfo(filename).Directory
            };
            var doc     = XDocument.Load(filename);
            var docRoot = doc.Root;
            if (docRoot == null) return project;

            var ns = docRoot.Name.Namespace;

            foreach (var i in docRoot.Elements(ns + "PropertyGroup"))
                project.ParsePropertyGroup(i);
            var itemGroups = docRoot.Elements(ns + "ItemGroup").ToArray();
            foreach (var ig in itemGroups)
                /*
                     *  <Reference Include="System" />
        <Reference Include="System.Core" />
        <Reference Include="System.Xml.Linq" />
        <Reference Include="System.Data.DataSetExtensions" />
        <Reference Include="Microsoft.CSharp" />
        <Reference Include="System.Data" />
        <Reference Include="System.Xml" />
                     */
            foreach (var i in ig.Elements())
                if (i.Name == ns + "Reference")
                {
                    project.Reference.Add((string)i.Attribute("Include"));
                }
                else if (i.Name == ns + "Compile")
                {
                    var name = (string)i.Attribute("Include");
                    var pi   = new ProjectItem(name, BuildActions.Compile);
                    project.Items.Add(pi);
                }
                else if (i.Name == ns + "ProjectReference")
                {
                    var g = ProjectReference.Deserialize(i);
                    project.ProjectReferences.Add(g);
                }
                else if (i.Name == ns + "Content")
                {
                    var name = (string)i.Attribute("Include");
                    var pi   = new ProjectItem(name, BuildActions.Content);
                    project.Items.Add(pi);
                }

            return project;
        }

        // Public Methods 

        // Private Methods 

        private void ParsePropertyGroup(XElement xElement)
        {
            var a = PropertyGroup.Deserialize(xElement);
            if (!string.IsNullOrEmpty(a.OutputType))
            {
                _outputType   = a.OutputType;
                _assemblyName = a.AssemblyName;
                if (a.ProjectGuid.HasValue)
                    ProjectGuid = a.ProjectGuid.Value;
            }
            else
            {
                PropertyGroups.Add(a);
            }
        }

        /// <summary>
        /// </summary>
        public DirectoryInfo WorkingDirectory { get; set; }

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
            set => _outputType = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public Guid ProjectGuid { get; set; }

        /// <summary>
        /// </summary>
        public List<Assembly> ReferencedAssemblies { get; set; } = new List<Assembly>();

        /// <summary>
        /// </summary>
        public List<string> Reference { get; set; } = new List<string>();

        /// <summary>
        /// </summary>
        public List<ProjectReference> ProjectReferences { get; set; } = new List<ProjectReference>();

        /// <summary>
        /// </summary>
        public List<PropertyGroup> PropertyGroups { get; set; } = new List<PropertyGroup>();

        /// <summary>
        /// </summary>
        public List<ProjectItem> Items { get; set; } = new List<ProjectItem>();

        private string _assemblyName = string.Empty;
        private string _outputType   = string.Empty;
    }
}