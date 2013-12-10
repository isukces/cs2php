using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [EnumRender(EnumRenderOptions.UnderscoreUppercase)]
    public enum MySQLFieldTypes
    {
        //numerics
        //-------------
        Bit = 16,
        Tinyint = 1,
        Bool = 1,
        Smallint = 2,
        Mediumint = 9,
        Integer = 3,
        Bigint = 8,
        Serial = 8,
        Float = 4,
        Double = 5,
        Decimal = 246,
        Numeric = 246,
        Fixed = 246,
        // dates
        // ------------
        Date = 10,
        Datetime = 12,
        Timestamp = 7,
        Time = 11,
        Year = 13,

        //Strings & Binary
        // ------------
        Char = 254,
        Varchar = 253,
        Enum = 254,
        Set = 254,
        Binary = 254,
        Varbinary = 253,
        Tinyblob = 252,
        Blob = 252,
        Mediumblob = 252,
        Tinytext = 252,
        Text = 252,
        Mediumtext = 252,
        Longtext = 252
    }
}
