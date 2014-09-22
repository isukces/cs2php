// ReSharper disable once CheckNamespace
namespace Lang.Php
{
    [AsArray]
    public class PosixUserInfo
    {
        /// <summary>
        /// The name element contains the username of the user. This is a short, usually less than 16 character "handle" of the user, not the real, full name.
        /// </summary>
        [ScriptName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The passwd element contains the user's password in an encrypted format. Often, for example on a system employing "shadow" passwords, an asterisk is returned instead.
        /// </summary>
        [ScriptName("passwd")]
        public string Password { get; set; }

        /// <summary>
        /// User ID, should be the same as the uid parameter used when calling the function, and hence redundant.
        /// </summary>
        [ScriptName("uid")]
        public int UserId { get; set; }

        /// <summary>
        /// The group ID of the user. Use the function posix_getgrgid() to resolve the group name and a list of its members.
        /// </summary>
        [ScriptName("gid")]
        public int GroupId { get; set; }

        /// <summary>
        /// GECOS is an obsolete term that refers to the finger information field on a Honeywell batch processing system. 
        /// The field, however, lives on, and its contents have been formalized by POSIX. 
        /// The field contains a comma separated list containing the user's full name, office phone, office number, and home phone number. On most systems, only the user's full name is available.
        /// </summary>
        [ScriptName("gecos")]
        public string Gecos { get; set; }

        /// <summary>
        /// This element contains the absolute path to the home directory of the user.
        /// </summary>
        [ScriptName("dir")]
        public string HomeDirectory { get; set; }

        /// <summary>
        /// The shell element contains the absolute path to the executable of the user's default shell. 
        /// </summary>
        [ScriptName("shell")]
        public string Shell { get; set; }
 
    }
}