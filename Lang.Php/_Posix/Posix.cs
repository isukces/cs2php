using System;

// ReSharper disable once CheckNamespace
namespace Lang.Php
{
    public static class Posix
    {
		#region Static Methods 

		// Public Methods 

        [DirectCall("posix_getgrgid")]
        public static Falsable<PosixGroupInfo> GetGroupById(int groupId)
        {
            return new PosixGroupInfo
            {
                GroupId = groupId
            };
        }

        [DirectCall("posix_getgrnam")]
        public static Falsable<PosixGroupInfo> GetGroupByName(string name)
        {
            return new PosixGroupInfo
            {
                Name = name
            };
        }

        /// <summary>
        /// Returns an array of information about the user referenced by the given user ID. 
        /// </summary>
        /// <param name="uid"></param>
        [DirectCall("posix_getpwuid")]
        public static Falsable<PosixUserInfo> GetUserById(int uid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an array of information about the given user. 
        /// </summary>
        /// <param name="username">An alphanumeric username.</param>
        [DirectCall("posix_getpwnam")]
        public static Falsable<PosixUserInfo> GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

		#endregion Static Methods 
    }
}
