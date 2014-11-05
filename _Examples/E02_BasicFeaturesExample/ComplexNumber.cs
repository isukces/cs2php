using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Examples.BasicFeaturesExample
{
    [Module("class.complex-number")]
    public class ComplexNumber
    {
        #region Constructors

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        [ScriptName("minus")]
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        [ScriptName("plus")]
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static implicit operator ComplexNumber(double x)
        {
            return new ComplexNumber(x, 0);
        }

        #endregion Static Methods

        #region Properties

        public double Imaginary { get; set; }

        public double Real { get; set; }

        #endregion Properties

        [ScriptName("mul1")]
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }
        [ScriptName("mul2")]
        public static ComplexNumber operator *(ComplexNumber a, double b)
        {
            return new ComplexNumber(a.Real * b, a.Imaginary * b);
        }
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            var tmp = b.Real * b.Real + b.Imaginary * b.Imaginary;
            var re = a.Real * b.Real + a.Imaginary * b.Imaginary;
            var im = a.Imaginary * b.Real - a.Real * b.Imaginary;
            return new ComplexNumber(re / tmp, im / tmp);
        }
    }
}
