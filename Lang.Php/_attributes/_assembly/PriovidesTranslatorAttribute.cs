using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class PriovidesTranslatorAttribute : Attribute
    {
        public PriovidesTranslatorAttribute(string TranslatorForAssembly)
        {
            this.TranslatorForAssembly = Guid.Parse(TranslatorForAssembly);
        }
        public Guid TranslatorForAssembly { get; private set; }
    }
}
