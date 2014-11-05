using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lang.Php.Runtime
{
    public class RuntimePhpDirectory : PhpDirectory
    {
        #region Constructors

        public RuntimePhpDirectory(string dirName)
        {
            dirName = Path.Combine(
                Script.Server.ContextDocumentRoot.Replace("/", "\\"),
                dirName.Replace("\\", "/"));

            _dir = new DirectoryInfo(dirName);
            items = new List<PhpDirectoryEntry>();
            items.Add(new PhpDirectoryEntry(".", _dir.FullName, true));
            items.Add(new PhpDirectoryEntry("..", _dir.Parent.FullName, true));


            var f = _dir.GetFiles().Select(i => new PhpDirectoryEntry(i.Name, i.FullName, false));
            var d = _dir.GetDirectories().Select(i => new PhpDirectoryEntry(i.Name, i.FullName, true));

            items.AddRange(d.Union(f).OrderBy(i => i.Name));
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public override bool ReadDir(out PhpDirectoryEntry file)
        {
            if (iterator >= items.Count)
            {
                file = null;
                return false;
            }
            file = items[iterator++];
            return true;
        }

        #endregion Methods

        #region Fields

        DirectoryInfo _dir;
        List<PhpDirectoryEntry> items;
        int iterator = 0;

        #endregion Fields

        #region Properties

        public override bool IsOk
        {
            get
            {
                return _dir != null && _dir.Exists;
            }
        }

        #endregion Properties

      

    }
}
