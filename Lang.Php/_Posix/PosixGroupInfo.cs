// ReSharper disable once CheckNamespace
namespace Lang.Php
{
    [AsArray]
    public class PosixGroupInfo
    {
        /// <summary>
        /// The name element contains the name of the group. This is a short, usually less than 16 character "handle" of the group, not the real, full name. 
        /// </summary>
        [ScriptName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The passwd element contains the group's password in an encrypted format. Often, for example on a system employing "shadow" passwords, an asterisk is returned instead. 
        /// </summary>
        [ScriptName("passwd")]
        public string Password { get; set; }

        /// <summary>
        /// Group ID, should be the same as the gid parameter used when calling the function, and hence redundant. 
        /// </summary>
        [ScriptName("gid")]
        public int GroupId { get; set; }

        /// <summary>
        ///  This consists of an array of string's for all the members in the group. 
        /// </summary>
        [ScriptName("members")]
        public string[] Members { get; set; }
    }
}