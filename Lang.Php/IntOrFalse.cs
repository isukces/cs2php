namespace Lang.Php
{
    [Skip]
    public class IntOrFalse
    {
		#region Constructors 

        private IntOrFalse()
        {

        }

		#endregion Constructors 

		#region Static Methods 

		// Public Methods 

        public static bool operator !=(IntOrFalse a, IntOrFalse b)
        {
            if (a.isFalse && b.isFalse) return false;
            if (a.isFalse) return a != b._value;
            if (b.isFalse) return b != a._value;
            return a._value != b._value;
        }

        public static bool operator !=(IntOrFalse a, int b)
        {
            if (a.isFalse)
                return b != 0;
            return a._value != b;
        }

          [UseOperator("!==")]
        public static bool operator !=(IntOrFalse a, bool b)
        {
            if (a.isFalse) return b;
            if (b)
                return a._value == 0;
            return a._value != 0;
        }

          [UseOperator("!==")]
          public static bool operator !=(bool b, IntOrFalse a)
          {
              if (a.isFalse) return b;
              if (b)
                  return a._value == 0;
              return a._value != 0;
          }

        public static bool operator !=(int b, IntOrFalse a)
        {
            if (a.isFalse)
                return b != 0;
            return a._value != b;
        }

        public static bool operator ==(IntOrFalse a, int b)
        {
            if (a.isFalse)
                return b == 0;
            return a._value == b;
        }
        public static implicit operator int(IntOrFalse a)
        {
            if (a.isFalse)
                return 0;
            return a._value;
        }
        public static bool operator ==(IntOrFalse a, IntOrFalse b)
        {
            if (a.isFalse && b.isFalse) return true;
            if (a.isFalse) return a == b._value;
            if (b.isFalse) return b == a._value;
            return a._value == b._value;
        }

          [UseOperator("===")]
        public static bool operator ==(IntOrFalse a, bool b)
        {
            if (a.isFalse) return !b;
            if (b)
                return a._value != 0;
            return a._value == 0;
        }

          [UseOperator("===")]
          public static bool operator ==( bool b, IntOrFalse a)
          {
              if (a.isFalse) return !b;
              if (b)
                  return a._value != 0;
              return a._value == 0;
          }

        public static bool operator ==(int b, IntOrFalse a)
        {
            if (a.isFalse)
                return b == 0;
            return a._value == b;
        }

        public static implicit operator IntOrFalse(int src)
        {
            return new IntOrFalse() { _value = src };
        }

		#endregion Static Methods 

		#region Static Fields 

        public static readonly IntOrFalse False = new IntOrFalse() { isFalse = true };

		#endregion Static Fields 

		#region Fields 

        int _value;
        bool isFalse;

		#endregion Fields 

		#region Properties 

        public int Value
        {
            get
            {
                return _value;
            }
        }

		#endregion Properties 
    }
}
