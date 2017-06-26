using System.IO;
using System;

namespace Lang.Php.Framework.Replacers
{
	[Replace(typeof(Math))]
	internal partial class MathReplacer
    {
	        [DirectCall("abs")]
        public static SByte Abs(SByte x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static Int16 Abs(Int16 x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static int Abs(int x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static long Abs(long x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static Single Abs(Single x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static double Abs(double x)
        {
            return Math.Abs(x);
        }

        [DirectCall("abs")]
        public static decimal Abs(decimal x)
        {
            return Math.Abs(x);
        }

        [DirectCall("acos")]
        public static double Acos(double x)
        {
            return Math.Acos(x);
        }

        [DirectCall("asin")]
        public static double Asin(double x)
        {
            return Math.Asin(x);
        }

        [DirectCall("atan")]
        public static double Atan(double x)
        {
            return Math.Atan(x);
        }

        [DirectCall("ceil")]
        public static decimal Ceiling(decimal x)
        {
            return Math.Ceiling(x);
        }

        [DirectCall("ceil")]
        public static double Ceiling(double x)
        {
            return Math.Ceiling(x);
        }

        [DirectCall("cos")]
        public static double Cos(double x)
        {
            return Math.Cos(x);
        }

        [DirectCall("cosh")]
        public static double Cosh(double x)
        {
            return Math.Cosh(x);
        }

        [DirectCall("exp")]
        public static double Exp(double x)
        {
            return Math.Exp(x);
        }

        [DirectCall("floor")]
        public static decimal Floor(decimal x)
        {
            return Math.Floor(x);
        }

        [DirectCall("floor")]
        public static double Floor(double x)
        {
            return Math.Floor(x);
        }

        [DirectCall("log")]
        public static double Log(double x)
        {
            return Math.Log(x);
        }

        [DirectCall("log10")]
        public static double Log10(double x)
        {
            return Math.Log10(x);
        }

        [DirectCall("round")]
        public static double Round(double x)
        {
            return Math.Round(x);
        }

        [DirectCall("round")]
        public static decimal Round(decimal x)
        {
            return Math.Round(x);
        }

        [DirectCall("sin")]
        public static double Sin(double x)
        {
            return Math.Sin(x);
        }

        [DirectCall("sinh")]
        public static double Sinh(double x)
        {
            return Math.Sinh(x);
        }

        [DirectCall("sqrt")]
        public static double Sqrt(double x)
        {
            return Math.Sqrt(x);
        }

        [DirectCall("tan")]
        public static double Tan(double x)
        {
            return Math.Tan(x);
        }

        [DirectCall("tanh")]
        public static double Tanh(double x)
        {
            return Math.Tanh(x);
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

	[Replace(typeof(File))]
	internal partial class System__IO__FileReplacer
    {
	        [DirectCall("file_exists")]
        public static bool Exists(string x)
        {
            return File.Exists(x);
        }

	}

}
