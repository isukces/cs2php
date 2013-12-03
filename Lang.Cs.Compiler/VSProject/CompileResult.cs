using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    
    property CompiledAssembly Assembly 
    
    property OutputAssemblyFilename string 
    
    property ProjectGuid Guid 
    smartClassEnd
    */
    
    public partial class CompileResult
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 12:39
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class CompileResult 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CompileResult()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CompiledAssembly## ##OutputAssemblyFilename## ##ProjectGuid##
        implement ToString CompiledAssembly=##CompiledAssembly##, OutputAssemblyFilename=##OutputAssemblyFilename##, ProjectGuid=##ProjectGuid##
        implement equals CompiledAssembly, OutputAssemblyFilename, ProjectGuid
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności CompiledAssembly; 
        /// </summary>
        public const string PROPERTYNAME_COMPILEDASSEMBLY = "CompiledAssembly";
        /// <summary>
        /// Nazwa własności OutputAssemblyFilename; 
        /// </summary>
        public const string PROPERTYNAME_OUTPUTASSEMBLYFILENAME = "OutputAssemblyFilename";
        /// <summary>
        /// Nazwa własności ProjectGuid; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTGUID = "ProjectGuid";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Assembly CompiledAssembly
        {
            get
            {
                return compiledAssembly;
            }
            set
            {
                compiledAssembly = value;
            }
        }
        private Assembly compiledAssembly;
        /// <summary>
        /// 
        /// </summary>
        public string OutputAssemblyFilename
        {
            get
            {
                return outputAssemblyFilename;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                outputAssemblyFilename = value;
            }
        }
        private string outputAssemblyFilename = string.Empty;
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
        #endregion Properties

    }
}
