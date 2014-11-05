using Lang.Php.Compiler.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{
    public abstract class PhpSourceBase
    {
        public static bool EqualCode<T>(T a, T b) where T : class
        {
            if ((a is IPhpValue || a == null) && (b is IPhpValue || b == null))
            {
                var codeA = a == null ? "" : (a as IPhpValue).GetPhpCode(null);
                var codeB = a == null ? "" : (b as IPhpValue).GetPhpCode(null);
                return codeA == codeB;
            }
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return a == b;
        }
        public static bool EqualCode_Array<T>(T[] a, T[] b) where T : class
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;
            for (var i = 0; i < a.Length; i++)
                if (!EqualCode(a[i], b[i]))
                    return false;
            return true;
        }
        public static bool EqualCode_List<T>(List<T> a, List<T> b) where T : class
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;
            for (var i = 0; i < a.Count; i++)
                if (!EqualCode(a[i], b[i]))
                    return false;
            return true;
        }
        public PhpSourceItems Kind
        {
            get
            {
                return PhpBaseVisitor<int>.GetKind(this);
            }
        }
    }
}
