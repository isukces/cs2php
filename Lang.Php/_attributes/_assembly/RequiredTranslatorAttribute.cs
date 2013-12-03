using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RequiredTranslatorAttribute : Attribute
    {
        /// <summary>
        /// Tworzy atrybut oznaczający, że dołączana biblioteka wymaga helpera to cs2php
        /// </summary>
        /// <param name="uniqueId">Unikalny ID assembly</param>
        /// <param name="suggested">Sugerowana nazwa translatora</param>
        public RequiredTranslatorAttribute(string suggested)
        {
            //   this.UniqueId = uniqueId;
            this.Suggested = suggested;
        }

        // public Guid UniqueId { get; private set; }

        public string Suggested { get; private set; }
    }
}
