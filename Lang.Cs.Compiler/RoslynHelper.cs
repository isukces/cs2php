using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler
{
     public class RoslynHelper
    {
        public static Assembly CompileAssembly(Compilation compilation, out EmitResult result)
        {
           
            using (var memoryStream = new MemoryStream())
            {
                result = compilation.Emit(memoryStream);
                if (!result.Success)
                    return null;
                
                memoryStream.Flush();
                var g = memoryStream.GetBuffer();
                var g2 = memoryStream.ToArray();
                var sss = Assembly.Load(g2);
                return sss;
            }

        }
    }
}
