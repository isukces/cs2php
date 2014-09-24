using Lang.Php;

namespace E05_Firebird
{
    [AsArray]
    [Skip]
    class Person
    {
        [AsValue]
        public const string FieldNames = FnId + "," + FnFirstName + "," + FnLastName;

        [AsValue]
        public const string FnId = "ID";
        [AsValue]
        public const string FnFirstName = "FIRSTNAME";
        [AsValue]
        public const string FnLastName = "LASTNAME";
        [ScriptName(FnId)]
        public int Id { get; set; }

        [ScriptName(FnFirstName)]
        public string FirstName { get; set; }
        [ScriptName(FnLastName)]
        public string LastnNme { get; set; }
    }
}
