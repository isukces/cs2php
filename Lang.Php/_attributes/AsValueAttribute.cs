using System;

namespace Lang.Php
{
    /// <summary>
    ///     Atrybut określa, że stała nie będzie nigdzie deklarowana, a wszystkie jej wystąpienia zostaną zastąpione watościami
    ///     <remarks>
    ///         Własność glue dla przykładu
    ///         const string CONST = "Some tekst"
    ///         x = "Mój tekst" + CONST;
    ///         jeśli glue = false
    ///         $x = 'Mój tekst' . 'Some tekst'
    ///         jeśli glue = true
    ///         $x = 'Mój tekstSome tekst'
    ///     </remarks>
    /// </summary>
    public class AsValueAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        /// </summary>
        public AsValueAttribute()
        {
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="glue">
        ///         Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w
        ///         wyrażeniu z innym tekstem)
        ///     </param>
        /// </summary>
        public AsValueAttribute(bool glue)
        {
            Glue = glue;
        }


        /// <summary>
        ///     Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w wyrażeniu z innym tekstem)
        /// </summary>
        public bool Glue { get; set; }
    }
}