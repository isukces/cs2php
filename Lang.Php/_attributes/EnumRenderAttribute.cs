using System;

namespace Lang.Php
{
    public class EnumRenderAttribute : Attribute
    {
        public EnumRenderAttribute(EnumRenderOptions o, bool DefinedConst)
        {
            Option = o;
            this.DefinedConst = DefinedConst;
        }
        public EnumRenderOptions Option { get; private set; }
        public bool DefinedConst { get; private set; }
    }
}
