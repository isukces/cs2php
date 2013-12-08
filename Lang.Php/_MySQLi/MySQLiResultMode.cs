using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php 
{
    public enum MySQLiResultMode
    {
        [RenderValue("MYSQLI_USE_RESULT")]
        UseResult,
        [RenderValue("MYSQLI_STORE_RESULT")]
        StoreResult
    }
}
