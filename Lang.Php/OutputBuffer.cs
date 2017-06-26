using System;

namespace Lang.Php
{
    public static class OutputBuffer
    {
        [DirectCall("ob_get_clean")]
        public static string GetClean()
        {
            throw new NotImplementedException();
        }

        [DirectCall("ob_end_clean")]
        public static string EndClean()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_end_flush")]
        public static string EndFlush()
        {
            throw new NotImplementedException();
        }

        [DirectCall("ob_start")]
        public static void Start()
        {
            // bool ob_start ([ callable $output_callback = NULL [, int $chunk_size = 0 [, int $flags = PHP_OUTPUT_HANDLER_STDFLAGS ]]] )
        }

        [DirectCall("ob_start")]
        public static void Start(Func<string, string> callback)
        {
            // bool ob_start ([ callable $output_callback = NULL [, int $chunk_size = 0 [, int $flags = PHP_OUTPUT_HANDLER_STDFLAGS ]]] )
        }
    }
}
