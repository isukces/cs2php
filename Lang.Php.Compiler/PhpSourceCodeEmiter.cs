using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public class PhpSourceCodeEmiter
    {
        PhpSourceCodeWriter code = new PhpSourceCodeWriter();
        PhpCodeModule _module;
        EmitContext _context;

        public static string GetAccessModifiers(IClassMember m)
        {
            List<string> a = new List<string>();
            if (m.Visibility == Visibility.Private)
                a.Add("private");
            else if (m.Visibility == Visibility.Protected)
                a.Add("protected");
            else a.Add("public");
            if (m.IsStatic)
                a.Add("static");
            return string.Join(" ", a);

        }


    }
}
