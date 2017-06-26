using System;

namespace Lang.Php.Wp 
{
    public class HookAttribute:Attribute
    {
        public HookAttribute(Hooks hook)
        {
            Hook = hook;
        }
        public Hooks Hook { get; private set; }
    }
}
