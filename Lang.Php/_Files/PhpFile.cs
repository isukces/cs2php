using System;

namespace Lang.Php
{
    public class PhpFile
    {
        #region Static Methods

        // Public Methods 

        [DirectCall("is_dir")]
        public static bool IsDir(string file)
        {
            throw new NotImplementedException();
        }

        [DirectCall("", "0")]
        public static implicit operator PhpFile(string filename)
        {
            return new PhpFile
            {
                FileName = filename
            };
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        [DirectCall("chmod", "this,0")]
        public void ChMod(UnixFilePermissions i)
        {
            throw new NotImplementedException();
        }

        [DirectCall("file_get_contents", "this,0")]
        public Falsable<string> GetContents(bool useIncludePath = false)
        {
            return PhpDummy.file_get_contents(FileName, useIncludePath);
        }

        [DirectCall("file_put_contents", "this,0")]
        public Falsable<int> PutContents(string data)
        {
            /* [DirectCall("file_put_contents")]
            public static Falsable<int> file_put_contents(string filename, string data)
            {
                throw new NotImplementedException();
                // int file_put_contents ( string $filename , mixed $data [, int $flags = 0 [, resource $context ]] )
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods

        #region Properties

        [DirectCall("fileatime", "this")]
        public UnixTimestamp ATime { get; set; }

        [DirectCall("filectime", "this")]
        public UnixTimestamp CTime { get; set; }

        [DirectCall("file_exists", "this")]
        public bool Exists { get; set; }

        [DirectCall("this")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets the file group. The group ID is returned in numerical format, use posix_getgrgid() to resolve it to a group name. 
        /// </summary>
        /// <returns>Returns the group ID of the file, or FALSE if an error occurs. The group ID is returned in numerical format, use posix_getgrgid() to resolve it to a group name. Upon failure, FALSE is returned. </returns>
        [DirectCall("filegroup ", "this")]
        public Falsable<int> GroupId { get; private set; }

        [DirectCall("filemtime", "this")]
        public UnixTimestamp MTime { get; set; }

        /// <summary>
        /// Gets the file owner. Returns the user ID of the owner of the file, or FALSE on failure. The user ID is returned in numerical format, use posix_getpwuid() to resolve it to a username. 
        /// </summary>
        [DirectCall("fileowner", "this")]
        public Falsable<int> OwnerId { get; private set; }

        [DirectCall("fileperms", "this")]
        public int Perms { get; set; }

        /// <summary>
        /// Calculates the MD5 hash of the file
        /// </summary>
        /// <param name="rawOutput">When TRUE, returns the digest in raw binary format with a length of 16</param>
        /// <returns></returns>
        [DirectCall("md5_file", "this,0")]
        public string Md5(bool rawOutput = false)
        {
            throw new NotSupportedException();
        }

        #endregion Properties

        [DirectCall("", "0")]
        public static PhpFile FromName(string fileName)
        {
            return new PhpFile
            {
                FileName = fileName
            };
        }
    }
}
