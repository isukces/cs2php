using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{

    [Since("5.3.0")]
    public enum RoundMode
    {

        /// <summary>
        /// Round halves up
        /// </summary>
        [RenderValue("PHP_ROUND_HALF_UP")]
        Up = 1,

        /// <summary>
        /// Round halves down
        /// </summary>
        [RenderValue("PHP_ROUND_HALF_DOWN")]
        Down = 2,

        /// <summary>
        /// ound halves to even numbers
        /// </summary>
        [RenderValue("PHP_ROUND_HALF_EVEN")]

        Even = 3,
        /// <summary>
        /// Round halves to odd numbers
        /// </summary>
        [RenderValue("PHP_ROUND_HALF_ODD")]
        Odd = 4
    }
}
