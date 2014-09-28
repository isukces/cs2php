using System;
using System.Collections.Generic;
using System.Text;

namespace Lang.Php.Wp 
{
    public class HookAttribute:Attribute
    {
        public HookAttribute(Hooks hook)
        {
            this.Hook = hook;
        }
        public Hooks Hook { get; private set; }
    }
}
