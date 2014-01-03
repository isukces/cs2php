using Lang.Php.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php.Compiler.Translator;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lang.Php.Test.Tests
{
#if !CS2PHP
    [Skip]
    public class BasicTests : Base
    {
        #region Static Methods

        // Public Methods 



        [Fact]
        public static void Compile()
        {
            var translator = Base.PrepareTranslator();
            var m = string.Join(", ", translator.Modules.Select(i => i.Name.Name).OrderBy(i => i));
            Assert.True(m == "Lang_Php_Test_Code_MyCode", m);

            MethodTranslation(MODULE_MYCODE, CLASS_MYCODE, "BasicMath1");
            MethodTranslation(MODULE_MYCODE, CLASS_MYCODE, "Collections");
            MethodTranslation(MODULE_MYCODE, CLASS_MYCODE, "CostantsAndVariables");
        }

        [Fact]
        public static string CSharpProject()
        {
            var csproj = Base.CsProj;
            Assert.True(File.Exists(csproj), "C# project not found");
            return csproj;
        }
        // Private Methods 



        #endregion Static Methods

        #region Fields

        const string CLASS_MYCODE = "\\MyCodePhp";
        const string MODULE_MYCODE = "Lang_Php_Test_Code_MyCode";

        #endregion Fields
    }
#endif
}
