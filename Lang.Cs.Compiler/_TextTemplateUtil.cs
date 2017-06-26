using System.Collections.Generic;

namespace Lang.Cs.Compiler
{
#if DEBUG
    class _TextTemplateUtil
    {
        static void Write(string x) { }


        public List<string> classes;
        string baseClass;

        public void MakeClassBase()
        {
            Wlf("public class {0} {{", baseClass);
            Wlf("    public {0}Kinds TokenKind", baseClass);
            Wlf("    {{");
            Wlf("        get");
            Wlf("        {{");
            foreach (var i in classes)
                Wlf("            if (this is {0}) return {1}Kinds.{0}Kind;", i, baseClass);
            Wlf("        throw new NotSupportedException();");
            Wlf("        }}");
            Wlf("    }}");
            Wlf("}}");
            Wlf("public enum {0}Kinds {{", baseClass);
            foreach (var i in classes)
                Wlf("    {0}Kind,", i);
            Wlf("}}");

            Wlf("public class {0}Visitor<T> {{", baseClass);
            Wlf("        public T Visit(CSharpBase a)");
            Wlf("        {{");
            Wlf("            switch (a.TokenKind)");
            Wlf("            {{");
            foreach (var i in classes)
            {
                Wlf("                case {1}Kinds.{0}Kind:", i, baseClass);
                Wlf("                    return Visit{0}(a as {0});", i);
            }
            Wlf("            }}");
            Wlf("            throw new NotSupportedException();");
            Wlf("        }}");
            foreach (var i in classes)
            {
                Wlf("    public virtual T Visit{0}({0} src) {{", i);
                Wlf("        throw new NotSupportedException();");
                Wlf("    }}");
            }
            Wlf("}}");
        }


        static void Wlf(string x, params object[] o)
        {
            Write(string.Format(x, o) + "\r\n");
        }

        static string StrEnc(string x)
        {
            return "\"" + x.Replace("\"", "\\\"") + "\"";
        }

        void map5(string a)
        {
            var b = a.Split(',');
            Subst(b[0].Trim(), b[1].Trim(), b[2].Trim(), b[3].Trim(), b[4].Trim());
        }

        static void Subst(string phpFunctionName, string csFunctionName, string typeIn, string TypeOut, string originalType)
        {
            Wlf("        [DirectCall({0})]", StrEnc(phpFunctionName));
            Wlf("        public static {0} {1}({2} x)\r\n", TypeOut, csFunctionName, typeIn);
            Wlf("        {{");
            Wlf("            return {0}.{1}(x);\r\n", originalType, csFunctionName);
            Wlf("        }}");
            Wlf("");
        }
    }

#endif
}
