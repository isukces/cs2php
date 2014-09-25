namespace Lang.Php.Test.Code
{
    [IgnoreNamespace]
    public class SampleEmptyClass
    {
        public string InstanceField1;

        [ScriptName("instanceField2")]
        public string InstanceField2;

        public static int ClassField1;
        [ScriptName("classField2")]
        public static int ClassField2;

        [GlobalVariable]
        public static int ClassField3;
        [GlobalVariable]
        [ScriptName("classField4")]
        public static int ClassField4;


        public const string Const1 = "1";
        [AsDefinedConst]
        public const string Const2 = "2";
        [AsValue]
        public const string Const3 = "3";

        [ScriptName("xConst4")]
        public const string Const4 = "4";
        [ScriptName("xConst5")]
        [AsDefinedConst]
        public const string Const5 = "5";

        [ScriptName("xConst6")]
        [AsValue]
        public const string Const6 = "6";

        public   void Test1()
        {
            var co = Const1;
            co = Const2;
            co = Const3;
            co = Const4;
            co = Const5;
            co = Const6;
            var i = InstanceField1;
            i = InstanceField2;

            var c = ClassField1;
            c = ClassField2;
            c = ClassField3;
            c = ClassField4;

            ClassField1 = 1;
            ClassField2 = 2;
            ClassField3 = 3;
            ClassField4 = 4;
        }
    }
}
