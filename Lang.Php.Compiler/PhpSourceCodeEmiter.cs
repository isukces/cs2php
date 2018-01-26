using System.Collections.Generic;

namespace Lang.Php.Compiler
{
    public class PhpSourceCodeEmiter
    {
        PhpSourceCodeWriter _code = new PhpSourceCodeWriter();

        public static string GetAccessModifiers(IPhpClassMember m)
        {
            var modifiers = new List<string>();
            switch (m.Visibility)
            {
                case Visibility.Private:
                    modifiers.Add("private");
                    break;
                case Visibility.Protected:
                    modifiers.Add("protected");
                    break;
                default:
                    modifiers.Add("public");
                    break;
            }
            if (m.IsStatic)
                modifiers.Add("static");
            return string.Join(" ", modifiers);

        }


    }
}
