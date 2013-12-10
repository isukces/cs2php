using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class MySQLiParam<T>
    {
        [DirectCall("this")]
        public T Value { get; set; }
    }
}
