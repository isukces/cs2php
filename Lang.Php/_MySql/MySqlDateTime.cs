using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class MySqlDateTime
    {
        public string Value { get; set; }
        public MySqlDateTime(string dt)
        {
            Value = dt;
        }

        [ScriptName("A")]
        [DirectCall(null,"0")]
        public static implicit operator string(MySqlDateTime src)
        {
            return src.Value;
        }
        [ScriptName("B")]
        [DirectCall(null, "0")]
        public static implicit operator MySqlDateTime(string src)
        {
            return new MySqlDateTime(src);
        }

        [DirectCall("strtotime")]
        public static implicit operator DateTime(MySqlDateTime src)
        {
            // yyyy-mm-dd 
            var y = int.Parse(src.Value.Substring(0, 4));
            var m = int.Parse(src.Value.Substring(5, 2));
            var d = int.Parse(src.Value.Substring(8, 2));
            return new DateTime(y, m, d);
            //return (DateTime)g;
        }

        [DirectCall("jakas_funkcja_ktorej_jeszcze_nie_mam")]
        public static implicit operator MySqlDateTime(DateTime src)
        {
            throw new NotSupportedException();
        }
    }
}
