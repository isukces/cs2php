using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{

    [Skip]
    public class PhpDirectory
    {
        [UseBinaryExpression("!==", "this", "false")]
        public virtual bool IsOk
        {
            get
            {
                throw new NotSupportedException();
                // return _ok;
            }
        }

        protected PhpDirectory()
        {

        }
        [DirectCall("opendir")]
        public static PhpDirectory Make(string dirName)
        {           
            return new RuntimePhpDirectory(dirName);
        }
        [DirectCall("readdir", "0,this", 0)]
        public virtual bool ReadDir(out PhpDirectoryEntry file)
        {
            throw new MockMethodException();
            // readdir
        }

        [DirectCall("closedir", "this")]
        public void Close()
        {

        }
    }

}
