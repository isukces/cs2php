using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class PhpDirectoryEntry
    {
        #region Constructors

        public PhpDirectoryEntry(string name)
        {

        }

        internal PhpDirectoryEntry(string name, string FullName, bool isDir)
        {
            this.Name = name;
            this._FullName = FullName;
            this._IsDir = _IsDir;
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        [DirectCall("is_file")]
        public static bool PhpIsFile(string filename)
        {
            throw new NotImplementedException();

        }

        #endregion Static Methods

        #region Properties

        internal string _FullName { get; set; }

        internal bool _IsDir { get; set; }

        /// <summary>
        /// dirname equivalent
        /// </summary>
        [DirectCall("dirname", "this")]
        public string DirectoryName
        {
            get
            {
                if (_IsDir)
                    return new System.IO.DirectoryInfo(_FullName).Parent.FullName;
                return new System.IO.FileInfo(_FullName).Directory.FullName;
            }
        }

        [DirectCall("filesize", "this")]
        public int FileSize { get; set; }

        [DirectCall("is_dir", "this")]
        public virtual bool IsDirectory
        {
            get
            {
                return _IsDir;
            }
        }

        [DirectCall("is_file", "this")]
        public bool IsFile
        {
            get
            {
                return !_IsDir;
            }
        }

        [UseBinaryExpression("==", "this", "'..'")]
        public bool IsParentDirectory
        {
            get
            {
                return this.Name == "..";
            }
        }

        [UseBinaryExpression("==", "this", "'.'")]
        public bool IsThisDirectory
        {
            get
            {
                return this.Name == ".";
            }
        }

        [DirectCall("fileatime", "this")]
        public UnixTimestamp LastAccessed { get; set; }

        [DirectCall("filectime", "this")]
        public UnixTimestamp LastChanged { get; set; }

        [DirectCall(null)]
        public string Name { get; set; }

        #endregion Properties
    }
}
