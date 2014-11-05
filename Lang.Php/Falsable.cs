using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class Falsable<T>
    {
        #region Constructors

        private Falsable()
        {

        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 
        [DirectCall(null, "0")]
        public static implicit operator T(Falsable<T> a)
        {
            if (a.isFalse)
                return default(T);
            return a._value;
        }


        [UseBinaryExpression("===", "false", "$0")]
        public static bool CheckIsFalse(T obj)
        {
            throw new MockMethodException();
        }

        [UseBinaryExpression("!==", "false", "$0")]
        public static bool CheckIsNotFalse(T obj)
        {
            throw new MockMethodException();
        }


        [UseBinaryExpression("===", "this", "false")]
        public bool IsFalse
        {
            get
            {
                return isFalse;
            }
        }
        [UseBinaryExpression("!==", "this", "false")]
        public bool IsNotFalse
        {
            get
            {
                return !isFalse;
            }
        }

        /*
        public static bool operator !=(Falsable<T> a, Falsable<T> b)
        {
            if (a.isFalse && b.isFalse) return false;
            if (a.isFalse) return a != b._value;
            if (b.isFalse) return b != a._value;
            return a._value != b._value;
        }

        public static bool operator !=(Falsable<T> a, T b)
        {
            if (a.isFalse)
                return b != 0;
            return a._value != b;
        }

          [UseOperator("!==")]
        public static bool operator !=(Falsable<T> a, bool b)
        {
            if (a.isFalse) return b;
            if (b)
                return a._value == 0;
            return a._value != 0;
        }

          [UseOperator("!==")]
          public static bool operator !=(bool b, Falsable<T> a)
          {
              if (a.isFalse) return b;
              if (b)
                  return a._value == 0;
              return a._value != 0;
          }

        public static bool operator !=(T b, Falsable<T> a)
        {
            if (a.isFalse)
                return b != 0;
            return a._value != b;
        }

        public static bool operator ==(Falsable<T> a, T b)
        {
            if (a.isFalse)
                return b == 0;
            return a._value == b;
        }
        public static implicit operator int(Falsable<T> a)
        {
            if (a.isFalse)
                return 0;
            return a._value;
        }
        public static bool operator ==(Falsable<T> a, Falsable<T> b)
        {
            if (a.isFalse && b.isFalse) return true;
            if (a.isFalse) return a == b._value;
            if (b.isFalse) return b == a._value;
            return a._value == b._value;
        }

          [UseOperator("===")]
        public static bool operator ==(Falsable<T> a, bool b)
        {
            if (a.isFalse) return !b;
            if (b)
                return a._value != 0;
            return a._value == 0;
        }

          [UseOperator("===")]
          public static bool operator ==( bool b, Falsable<T> a)
          {
              if (a.isFalse) return !b;
              if (b)
                  return a._value != 0;
              return a._value == 0;
          }

        public static bool operator ==(T b, Falsable<T> a)
        {
            if (a.isFalse)
                return b == 0;
            return a._value == b;
        }
         */

        public static implicit operator Falsable<T>(T src)
        {
            return new Falsable<T>() { _value = src, isFalse = false };
        }

        #endregion Static Methods

        #region Static Fields

        public static readonly Falsable<T> False = new Falsable<T>() { isFalse = true };

        #endregion Static Fields

        #region Fields

        T _value;
        bool isFalse;

        #endregion Fields

        #region Properties

        [DirectCall("", "this")]
        public T Value
        {
            get
            {
                return _value;
            }
        }

        #endregion Properties
    }
}
