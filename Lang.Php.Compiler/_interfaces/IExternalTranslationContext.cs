using Lang.Cs.Compiler;
using Lang.Php.Compiler.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public interface IExternalTranslationContext
    {
        #region Operations

        IPhpValue TranslateValue(IValue srcValue);
        TranslationInfo GetTranslationInfo();

        ClassReplaceInfo FindOneClassReplacer(Type srcType);

        Version PhpVersion { get; }

        #endregion Operations
    }
}
