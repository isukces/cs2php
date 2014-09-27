using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Test.Code
{
    [IgnoreNamespace]
    public class BusinessClass
    {
        public static int ClassField;
        public static int ClassProperty { get; set; }
        public int InstanceField;
        public int InstanceProperty { get; set; }
    }
    [IgnoreNamespace]
    [Skip]
    public class BusinessClassDefinedConst
    {
        [AsDefinedConst]
        public static int ClassFieldDefinedConst = 3;

        [AsDefinedConst]
        [Module("other-module-BusinessClassDefinedConst")]
        public static int ClassFieldDefinedConstInOtherModule = 3;
    }

    [IgnoreNamespace]
    [Skip]
    public class BusinessClassGlobalVariable
    {
        [GlobalVariable]
        public static int ClassFieldGlobalVariable = 3;
    }

    [IgnoreNamespace]
    public class IncludeClassFieldAccess
    {
        public void X()
        {
            var a = BusinessClass.ClassField;
            var b1 = BusinessClassDefinedConst.ClassFieldDefinedConst;
            var b2 = BusinessClassDefinedConst.ClassFieldDefinedConstInOtherModule;
            var c = BusinessClassGlobalVariable.ClassFieldGlobalVariable;
        }
    }
    [IgnoreNamespace]
    public class IncludeClassPropertyAccess
    {
        public void X()
        {
            var a = BusinessClass.ClassProperty;
        }
    }

    [IgnoreNamespace]
    public class IncludeInstanceFieldAccess
    {
        public void X()
        {
            var a = new BusinessClass().InstanceField;
        }
    }
    [IgnoreNamespace]
    public class IncludeInstancePropertyAccess
    {
        public void X()
        {
            var a = new BusinessClass().InstanceProperty;
        }
    }

    [IgnoreNamespace]
    public class IncludeShouldNotIncludeOther : PhpDummy
    {
        public void X()
        {
            var a = PHP_EOL;
        }
    }
}
