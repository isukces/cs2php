using Lang.Php.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Lang.Php.Wp.Compile
{
    public class ModuleProcessor : IModuleProcessor
    {
        public void BeforeEmit(PhpCodeModule module, TranslationInfo info)
        {
            //var a = info.CurrentAssembly.GetCustomAttributes(false);
            var assemblyTI = AssemblyTranslationInfo.FromAssembly(info.CurrentAssembly);
            // var c = info.ClassTranslations.Values.Where(u => u.ModuleName != null && u.ModuleName.Library == assemblyTI.LibraryName).ToArray();
            var typesInThisModule = info.ClassTranslations.Values.Where(u => u.ModuleName != null && u.ModuleName == module.Name).ToArray();
            var typesWithAttribute = from i in typesInThisModule
                                     let _attribute = i.Type.GetCustomAttribute<MainPluginModuleAttribute>(false)
                                     where _attribute != null
                                     select i;
            if (typesWithAttribute.Any())
            {
                var PluginName = "Please fill AssemblyTrademark attribute";
                {
                    var a = info.CurrentAssembly.GetCustomAttribute<AssemblyTrademarkAttribute>();
                    if (a != null && !string.IsNullOrEmpty(a.Trademark)) PluginName = a.Trademark;
                }
                var Author = "";
                {
                    var a = info.CurrentAssembly.GetCustomAttribute<AssemblyCompanyAttribute>();
                    if (a != null && !string.IsNullOrEmpty(a.Company)) Author = a.Company;
                }
                var Description = "";
                {
                    var a = info.CurrentAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
                    if (a != null && !string.IsNullOrEmpty(a.Description)) Description = a.Description;
                }
                List<string> l = new List<string>();
                l.Add("Plugin Name: " + PluginName);
                l.Add("Author: " + Author);
                l.Add("Author URI: " + "???");
                l.Add("Description: " + Description);
                module.TopComments = string.Join("\r\n", l) + "\r\n" + module.TopComments;


            }
            // throw new NotImplementedException();
        }
    }
}
