using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{
	[Replace(typeof(Math))]
	internal partial class MathReplacer
    {
	        [DirectCall("abs")]
        public static System.SByte Abs(System.SByte x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static System.Int16 Abs(System.Int16 x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static int Abs(int x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static long Abs(long x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static System.Single Abs(System.Single x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static double Abs(double x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("abs")]
        public static decimal Abs(decimal x)
        {
            return System.Math.Abs(x);
        }

        [DirectCall("acos")]
        public static double Acos(double x)
        {
            return System.Math.Acos(x);
        }

        [DirectCall("asin")]
        public static double Asin(double x)
        {
            return System.Math.Asin(x);
        }

        [DirectCall("atan")]
        public static double Atan(double x)
        {
            return System.Math.Atan(x);
        }

        [DirectCall("ceil")]
        public static decimal Ceiling(decimal x)
        {
            return System.Math.Ceiling(x);
        }

        [DirectCall("ceil")]
        public static double Ceiling(double x)
        {
            return System.Math.Ceiling(x);
        }

        [DirectCall("cos")]
        public static double Cos(double x)
        {
            return System.Math.Cos(x);
        }

        [DirectCall("cosh")]
        public static double Cosh(double x)
        {
            return System.Math.Cosh(x);
        }

        [DirectCall("exp")]
        public static double Exp(double x)
        {
            return System.Math.Exp(x);
        }

        [DirectCall("floor")]
        public static decimal Floor(decimal x)
        {
            return System.Math.Floor(x);
        }

        [DirectCall("floor")]
        public static double Floor(double x)
        {
            return System.Math.Floor(x);
        }

        [DirectCall("log")]
        public static double Log(double x)
        {
            return System.Math.Log(x);
        }

        [DirectCall("log10")]
        public static double Log10(double x)
        {
            return System.Math.Log10(x);
        }

        [DirectCall("round")]
        public static double Round(double x)
        {
            return System.Math.Round(x);
        }

        [DirectCall("round")]
        public static decimal Round(decimal x)
        {
            return System.Math.Round(x);
        }

        [DirectCall("sin")]
        public static double Sin(double x)
        {
            return System.Math.Sin(x);
        }

        [DirectCall("sinh")]
        public static double Sinh(double x)
        {
            return System.Math.Sinh(x);
        }

        [DirectCall("sqrt")]
        public static double Sqrt(double x)
        {
            return System.Math.Sqrt(x);
        }

        [DirectCall("tan")]
        public static double Tan(double x)
        {
            return System.Math.Tan(x);
        }

        [DirectCall("tanh")]
        public static double Tanh(double x)
        {
            return System.Math.Tanh(x);
        }

	}

	[Replace(typeof(double))]
	internal partial class DoubleReplacer
    {
		[DirectCall("","0")]
        public static double Parse(string s)
        {
            return double.Parse(s);
        }
	        [DirectCall("is_infinite")]
        public static bool IsInfinity(double x)
        {
            return double.IsInfinity(x);
        }

        [DirectCall("is_nan")]
        public static bool IsNaN(double x)
        {
            return double.IsNaN(x);
        }

	}

	[Replace(typeof(System.IO.File))]
	internal partial class System__IO__FileReplacer
    {
	        [DirectCall("file_exists")]
        public static bool Exists(string x)
        {
            return System.IO.File.Exists(x);
        }

	}

}
