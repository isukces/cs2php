using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Lang.Php
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Assembly)]
    [Guid("1FA81F36-8B6D-483C-8407-88C4197F4FFC")]
    public class RequiredTranslatorAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Tworzy atrybut oznaczający, że dołączana biblioteka wymaga helpera to cs2php
        /// </summary>
        /// <param name="suggested">Sugerowana nazwa translatora</param>
        public RequiredTranslatorAttribute(string suggested)
        {
            Suggested = suggested;
        }

        #endregion Constructors

        #region Properties

        public string Suggested { get; private set; }

        #endregion Properties
    }
}
