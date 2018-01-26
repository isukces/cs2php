using System;
using System.Globalization;

namespace Lang.Php.Runtime
{
    public  class PhpCodeValue
    {
        // Public Methods 

        public static PhpCodeValue FromBool(bool v)
        {
            var txt = v ? "true" : "false";
            return new PhpCodeValue(txt, v, Kinds.Bool);
        }

        public static PhpCodeValue FromDouble(double v)
        {
            var txt = v.ToString(CultureInfo.InvariantCulture);
            return new PhpCodeValue(txt, v, Kinds.Double);
        }

        public static PhpCodeValue FromInt(int v, bool octal = false)
        {
            var txt = octal ? PhpValues.Dec2Oct(v) : v.ToString();
            return new PhpCodeValue(txt, v, octal ? Kinds.OctalInt : Kinds.Int);
        }

        public static PhpCodeValue FromString(string txt)
        {
            return new PhpCodeValue(PhpValues.PhpStringEmit(txt, true), txt, Kinds.StringConstant);
        }

        // Public Methods 

        public bool TryGetPhpString(out string txt)
        {
            switch (Kind)
            {
                case Kinds.StringConstant:
                    if (PhpValues.TryGetPhpStringValue(_phpValue, out txt))
                        return true;
                    throw new NotSupportedException();
                case Kinds.Bool:
                    txt = (bool)SourceValue ? "1" : "";
                    return true;
                case Kinds.Int:
                case Kinds.OctalInt:
                    txt = ((int)SourceValue).ToString();
                    return true;
                case Kinds.Double:
                    txt = _phpValue;
                    return true;
                case Kinds.DefinedConst:
                    txt = "";
                    return false;
                case Kinds.Null:
                    txt = "";
                    return true;
                default:
                    throw new NotSupportedException();

            }
        }

        public enum Kinds
        {
            Other, // ie. flags
            StringConstant,
            Int,
            OctalInt,
            DefinedConst,
            Bool,
            Double,
            Null
        }
   
      
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="phpValue">Value on PHP side</param>
        /// <param name="kind"></param>
        /// </summary>
        public PhpCodeValue(string phpValue, Kinds kind)
        {
            PhpValue = phpValue;
            Kind = kind;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="phpValue">Value on PHP side</param>
        /// <param name="sourceValue"></param>
        /// <param name="kind"></param>
        /// </summary>
        public PhpCodeValue(string phpValue, object sourceValue, Kinds kind)
        {
            PhpValue = phpValue;
            SourceValue = sourceValue;
            Kind = kind;
        }

     

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", _phpValue);
        }

        /// <summary>
        /// Value on PHP side
        /// </summary>
        public string PhpValue
        {
            get => _phpValue;
            set => _phpValue = (value ?? String.Empty).Trim();
        }
        private string _phpValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public object SourceValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Kinds Kind { get; set; }
    }
}
