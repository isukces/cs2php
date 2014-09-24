using System;

namespace Lang.Php
{
    [Flags]
    public enum IbaseTransactionOptions
    {
        [RenderValue("IBASE_READ")]
        Read,
        [RenderValue("IBASE_WRITE")]
        Write,
        [RenderValue("IBASE_COMMITTED")]
        Committed,
        [RenderValue("IBASE_CONSISTENCY")]
        Consistency,
        [RenderValue("IBASE_CONCURRENCY")]
        Concurrency,
        [RenderValue("IBASE_REC_VERSION")]
        RecVersion,
        [RenderValue("IBASE_REC_NO_VERSION")]
        RecNoVersion,
        [RenderValue("IBASE_WAIT")]
        Wait,
        [RenderValue("IBASE_NOWAIT")]
        Nowait
    }
}