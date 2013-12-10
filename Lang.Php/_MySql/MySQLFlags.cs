using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Flags]
    public enum MySQLFlags
    {
        [RenderValue("NOT_NULL_FLAG")]
        NotNull = 1,
        [RenderValue("PRI_KEY_FLAG")]
        PrimaryKey = 2,
        [RenderValue("UNIQUE_KEY_FLAG")]
        UniqueKey = 4,
        [RenderValue("BLOB_FLAG")]
        Blob = 16,
        [RenderValue("UNSIGNED_FLAG")]
        Unsigned = 32,
        [RenderValue("ZEROFILL_FLAG")]
        Zerofill = 64,
        [RenderValue("BINARY_FLAG")]
        Binary = 128,
        [RenderValue("ENUM_FLAG")]
        Enum = 256,
        [RenderValue("AUTO_INCREMENT_FLAG")]
        AutoIncrement = 512,
        [RenderValue("TIMESTAMP_FLAG")]
        Timestamp = 1024,
        [RenderValue("SET_FLAG")]
        Set = 2048,
        [RenderValue("NUM_FLAG")]
        Num = 32768,
        [RenderValue("PART_KEY_FLAG")]
        PartialKey = 16384,
        [RenderValue("GROUP_FLAG")]
        Group = 32768,
        [RenderValue("UNIQUE_FLAG")]
        Unique = 65536
    }
}
