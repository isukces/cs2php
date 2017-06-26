using System;
using System.IO;
using System.Text;

namespace Lang.Php
{
    public class PhpFiles
    {

      
        [DirectCall("file_get_contents")]
        public static Falsable<string> FileGetContents(string filename, bool use_include_path = false) {
              // string file_get_contents ( string $filename [, bool $use_include_path = false [, resource $context [, int $offset = -1 [, int $maxlen ]]]] )

            if (!File.Exists(filename))
                return Falsable<string>.False;
            try
            {
                var a = File.ReadAllBytes(filename);
                return Encoding.UTF8.GetString(a);
            }
            catch
            {
                return Falsable<string>.False;
            }
        }

        [DirectCall("readfile")]
        public static Falsable<int> ReadFile(string filename, bool use_include_path = false)
        {
            if (!File.Exists(filename))
                return Falsable<int>.False;
            try
            {
                var a = File.ReadAllBytes(filename);
                Console.Write(Encoding.UTF8.GetString(a));
                return a.Length;
            }
            catch
            {
                return Falsable<int>.False;
            }
        }

		#region Static Methods 

		// Public Methods 

        /// <summary>
        /// string basename ( string $path [, string $suffix ] )
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DirectCall("basename")]
        public static string BaseName(string path)
        {
            return BaseName(path, Path.PathSeparator.ToString());
        }

        /// <summary>
        /// string basename ( string $path [, string $suffix ] )
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DirectCall("basename")]
        [Since("4.1.0")]
        public static string BaseName(string path, string suffix)
        {
            var i = path.LastIndexOf(suffix);
            if (i < 0)
                return path;
            return path.Substring(0, i);
        }

        /// <summary>
        /// bool chgrp ( string $filename , mixed $group )
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DirectCall("chgrp")]
        [Obsolete("not tested !!!")]
        public static bool ChangeGroup(string filename, UnixFileGroupPermissions group)
        {
            return true;
        }

        /// <summary>
        /// bool chmod ( string $filename , int $mode )
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DirectCall("chmod")]
        public static bool ChangeMod(string filename, UnixFilePermissions mode)
        {
            return true;
        }

        /// <summary>
        /// bool chown ( string $filename , mixed $user )
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [DirectCall("chown")]
        public static bool ChangeOwner(string filename, string user)
        {
            return true;
        }

        /// <summary>
        /// void clearstatcache ([ bool $clear_realpath_cache = false [, string $filename ]] )
        /// </summary>
        /// <param name="clear_realpath_cache"></param>
        /// <param name="?"></param>
        [DirectCall("clearstatcache")]
        public static void ClearStatCache(bool clear_realpath_cache = false, string filename = "")
        {

        }

        /// <summary>
        /// bool copy ( string $source , string $dest [, resource $context ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("copy")]
        public static bool Copy(string source, string dest)
        {
            try
            {
                File.Copy(source, dest);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// bool unlink ( string $filename [, resource $context ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("unlink")]
        [Obsolete("Use 'unlink' instead")]
        public static bool Delete(string filename)
        {
            return Unlink(filename);
        }

        /// <summary>
        /// string dirname ( string $path )
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DirectCall("dirname")]
        public static string Dirname ( string path ) {
            // Returns the path of the parent directory. If there are no slashes in path, a dot ('.') is returned, indicating the current directory. Otherwise, the returned string is path with any trailing /component removed. 
            var i = path.LastIndexOf(Path.DirectorySeparatorChar.ToString());
            if (i < 0)
                return ".";
            return path.Substring(0, i);
    }

        /// <summary>
        /// int filesize ( string $filename )
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [DirectCall("filesize")]
        public static int FileSize(string filename)
        {
            if (!File.Exists(filename))
                return 0;
            return (int)new FileInfo(filename).Length;
        }

        /// <summary>
        /// bool mkdir ( string $pathname [, int $mode = 0777 [, bool $recursive = false [, resource $context ]]] )
        /// </summary>
        [DirectCall("mkdir")]
        public static bool MkDir(string pathname, UnixFilePermissions mode = CommonUnixFilePermissions.Perm0777)
        {
            try
            {
                Directory.CreateDirectory(pathname);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// bool move_uploaded_file ( string $filename , string $destination )
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [DirectCall("move_uploaded_file")]
        public static bool MoveUploadedFile(string filename, string destination)
        {
            try
            {
                File.Move(filename, destination);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// mixed pathinfo ( string $path [, int $options = PATHINFO_DIRNAME | PATHINFO_BASENAME | PATHINFO_EXTENSION | PATHINFO_FILENAME ] )
        /// </summary>
        [DirectCall("pathinfo")]
        public static PathInfoResult PathInfo(string path)
        {
#warning 'Implementacja możliwa';
            return new PathInfoResult();
        }

        /// <summary>
        /// mixed pathinfo ( string $path [, int $options = PATHINFO_DIRNAME | PATHINFO_BASENAME | PATHINFO_EXTENSION | PATHINFO_FILENAME ] )
        /// </summary>
        [DirectCall("pathinfo")]
        public static PathInfoResult PathInfo(string path, PathInfoOptions options)
        {
#warning 'Implementacja możliwa';
            return new PathInfoResult();
        }

        /// <summary>
        /// bool unlink ( string $filename [, resource $context ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("unlink")]
        public static bool Unlink(string filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch
            {
                return false;
            }
            return true;
        }

		#endregion Static Methods 
    }
}
