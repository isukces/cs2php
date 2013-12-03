using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Items List<Project> 
    	init #
    
    property SkipCompile Dictionary<Guid, string> zamiast kompilować, załaduj gotowce
    	init #
    smartClassEnd
    */

    public partial class ProjectCollection
    {
        public Dictionary<Guid, VSProject.CompileResult> CompileAll(string outDir)
        {
            var projectsToCompile = GetCompileOrder();
            Dictionary<Guid, VSProject.CompileResult> compiled = new Dictionary<Guid, VSProject.CompileResult>();
            foreach (var project in projectsToCompile)
            {
                if (skipCompile.ContainsKey(project.ProjectGuid))
                {
                    var loadFN = skipCompile[project.ProjectGuid];
                    compiled[project.ProjectGuid] = new CompileResult()
                    {
                        ProjectGuid = project.ProjectGuid,
                        CompiledAssembly = Assembly.LoadFile(loadFN),
                        OutputAssemblyFilename = loadFN
                    };
                    continue;
                }
                string[] compiledDLLofReferencedProjects = project.ProjectReferences.Select(ii => compiled[ii.ProjectGuid].OutputAssemblyFilename).ToArray();
                var compileResult = project.Compile(outDir, compiledDLLofReferencedProjects);
                compiled[compileResult.ProjectGuid] = compileResult;
            }
            return compiled;
        }
        public Project[] GetCompileOrder()
        {
            List<Project> result = new List<Project>();
            foreach (var project in items)
                Add(project, result);
            return result.ToArray();
        }

        void Add(Project project, List<Project> list)
        {
            if (list.Where(Q => Q.ProjectGuid == project.ProjectGuid).Any()) return;
            foreach (var rp in project.ProjectReferences)
            {
                var requestedGuid = rp.ProjectGuid;
                if (list.Where(ii => requestedGuid == ii.ProjectGuid).Any()) continue;
                var item = items.Where(ii => ii.ProjectGuid == requestedGuid).FirstOrDefault();
                Add(item, list);
            }
            list.Add(project);
        }


        public void Load(string csProjFilename)
        {
            VSProject.Project project = VSProject.Project.Load(csProjFilename);
            if (items.Where(i => i.AssemblyName == project.AssemblyName).Any())
                return;

            items.Add(project);
            if (project.ProjectReferences.Any())
            {
                var dir = new FileInfo(csProjFilename).Directory.FullName;
                foreach (var i in project.ProjectReferences)
                    Load(Path.Combine(dir, i.Path));
            }

        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 14:57
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class ProjectCollection
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ProjectCollection()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Items## ##SkipCompile##
        implement ToString Items=##Items##, SkipCompile=##SkipCompile##
        implement equals Items, SkipCompile
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Items; 
        /// </summary>
        public const string PROPERTYNAME_ITEMS = "Items";
        /// <summary>
        /// Nazwa własności SkipCompile; zamiast kompilować, załaduj gotowce
        /// </summary>
        public const string PROPERTYNAME_SKIPCOMPILE = "SkipCompile";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public List<Project> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
        private List<Project> items = new List<Project>();
        /// <summary>
        /// zamiast kompilować, załaduj gotowce
        /// </summary>
        public Dictionary<Guid, string> SkipCompile
        {
            get
            {
                return skipCompile;
            }
            set
            {
                skipCompile = value;
            }
        }
        private Dictionary<Guid, string> skipCompile = new Dictionary<Guid, string>();
        #endregion Properties

    }
}
