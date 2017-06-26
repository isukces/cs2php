using System;

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
