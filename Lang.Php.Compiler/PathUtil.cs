using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public static class PathUtil
    {
        public const string WIN_SEP = "\\";
        public const string UNIX_SEP = "/";
        public const string TWO_UNIX_SEP = "//";
        public const string TWO_WIN_SEP = "\\\\";

        public static string MakeUnixPath(string path)
        {
            path = path.Replace(WIN_SEP, UNIX_SEP);
            while (path.IndexOf(TWO_UNIX_SEP) > 0)
                path = path.Replace(TWO_UNIX_SEP, UNIX_SEP);
            return path;
        }
        public static string MakeWinPath(string path)
        {
            path = path.Replace(UNIX_SEP, WIN_SEP);
            while (path.IndexOf(TWO_WIN_SEP) > 0)
                path = path.Replace(TWO_WIN_SEP, WIN_SEP);
            return path;
        }

        public static IPhpValue MakePathValueRelatedToFile(KnownConstInfo value, TranslationInfo info)
        {
            if (value.Value == null)
            {
                info.Log(MessageLevels.Error,
                       string.Format("Const value {0} must be a string (currently is null)", value.Name));
                return new PhpConstValue("???");
            }
            if (value.Value is string)
                return MakePathValueRelatedToFile(value.Value as string);
            info.Log(MessageLevels.Warning,
                   string.Format("Const value {0} must be a string", value.Name));
            return new PhpConstValue("???");
        }
        public static IPhpValue MakePathValueRelatedToFile(string path)
        {
            path = MakeUnixPath(path + UNIX_SEP);
            if (!path.StartsWith(UNIX_SEP))
                path = UNIX_SEP + path;
            var _FILE_ = new PhpDefinedConstExpression("__FILE__", null);
            var dirinfo = new PhpMethodCallExpression("dirname", _FILE_);
            var a2 = new PhpConstValue(path);
            var concat = new PhpBinaryOperatorExpression(".", dirinfo, a2);
            return concat;
        }

       
        public static string MakeRelativePath(string path, string relTo)
        {
            Uri fromUri = new Uri(new DirectoryInfo(relTo + WIN_SEP).FullName);
            Uri toUri = new Uri(new DirectoryInfo(path + WIN_SEP).FullName);
            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath;
        }
    }
}
