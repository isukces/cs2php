using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class EnumRenderAttribute : Attribute
    {
        public EnumRenderAttribute(EnumRenderOptions o)
        {
            this.Option = o;
        }
        public EnumRenderOptions Option { get; private set; }
    }
}
