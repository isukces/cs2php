﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler 
{
    public enum StatementEmitInfo
    {
        NormalSingleStatement,
        Empty,
        ManyItemsOrPlainHtml
    }
}
