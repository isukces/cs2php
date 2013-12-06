using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public class TranslationMessage
    {
        public TranslationMessage(string Text, MessageLevels Level)
        {
            this.Text = Text;
            this.Level = Level;
        }
        public string Text { get; private set; }
        public MessageLevels Level { get; private set; }


    }

}
