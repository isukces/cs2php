using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler
{
#if DEBUG
    class _TextTemplateUtil
    {
        void Write(string x) { }


        public List<string> classes;
        string baseClass;

        public void MakeClassBase()
        {
            WLF("public class {0} {{", baseClass);
            WLF("    public {0}Kinds TokenKind", baseClass);
            WLF("    {{");
            WLF("        get");
            WLF("        {{");
            foreach (var i in classes)
                WLF("            if (this is {0}) return {1}Kinds.{0}Kind;", i, baseClass);
            WLF("        throw new NotSupportedException();");
            WLF("        }}");
            WLF("    }}");
            WLF("}}");
            WLF("public enum {0}Kinds {{", baseClass);
            foreach (var i in classes)
                WLF("    {0}Kind,", i);
            WLF("}}");

            WLF("public class {0}Visitor<T> {{", baseClass);
            WLF("        public T Visit(CSharpBase a)");
            WLF("        {{");
            WLF("            switch (a.TokenKind)");
            WLF("            {{");
            foreach (var i in classes)
            {
                WLF("                case {1}Kinds.{0}Kind:", i, baseClass);
                WLF("                    return Visit{0}(a as {0});", i);
            }
            WLF("            }}");
            WLF("            throw new NotSupportedException();");
            WLF("        }}");
            foreach (var i in classes)
            {
                WLF("    public virtual T Visit{0}({0} src) {{", i);
                WLF("        throw new NotSupportedException();");
                WLF("    }}");
            }
            WLF("}}");
        }




        void WLF(string x, params object[] o)
        {
            Write(string.Format(x, o) + "\r\n");
        }

        string strEnc(string x)
        {
            return "\"" + x.Replace("\"", "\\\"") + "\"";
        }

        void map5(string a)
        {
            var b = a.Split(',');
            subst(b[0].Trim(), b[1].Trim(), b[2].Trim(), b[3].Trim(), b[4].Trim());
        }
        void subst(string phpFunctionName, string csFunctionName, string typeIn, string TypeOut, string originalType)
        {
            WLF("        [DirectCall({0})]", strEnc(phpFunctionName));
            WLF("        public static {0} {1}({2} x)\r\n", TypeOut, csFunctionName, typeIn);
            WLF("        {{");
            WLF("            return {0}.{1}(x);\r\n", originalType, csFunctionName);
            WLF("        }}");
            WLF("");
        }
    }

#endif
}
