using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class FirebirdResult
    {

        [DirectCall("ibase_fetch_assoc", "this,0", 1)]
        public virtual bool FetchAssoc<T>(out T row)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// bool ibase_free_result ( resource $result_identifier )
        /// </summary>
        [DirectCall("ibase_free_result", "this")]
        public void FreeResult()
        {
            // bool ibase_free_result ( resource $result_identifier )
            // _phpLevelDisposed = true;
        }

        [UseBinaryExpression("!==", "this", "false")]
        public bool IsOk
        {
            get
            {
                throw new NotImplementedException();
                // return _ok;
            }
        }
    }
}
