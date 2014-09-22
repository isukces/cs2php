using System;

namespace Lang.Php
{
    [Flags]
    [EnumRender(EnumRenderOptions.OctalNumbers, false)]
    public enum UnixFilePermissions
    {
        None = 0,

        OtherRead = 4,
        OtherWrite = 2,
        OtherExec = 1,

        GroupRead = 32,
        GroupWrite = 16,
        GroupExec = 8,

        OwnerRead = 256,
        OwnerWrite = 128,
        OwnerExec = 64,

    }
    public static class CommonUnixFilePermissions {
    
        [AsValue]
        public const UnixFilePermissions Perm00 = UnixFilePermissions.None;
        [AsValue]
        public const UnixFilePermissions Perm01 = UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm02 = UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm03 = UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm04 = UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm05 = UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm06 = UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm07 = UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm010 = UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm011 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm012 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm013 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm014 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm015 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm016 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm017 = UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm020 = UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm021 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm022 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm023 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm024 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm025 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm026 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm027 = UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm030 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm031 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm032 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm033 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm034 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm035 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm036 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm037 = UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm040 = UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm041 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm042 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm043 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm044 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm045 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm046 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm047 = UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm050 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm051 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm052 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm053 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm054 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm055 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm056 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm057 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm060 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm061 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm062 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm063 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm064 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm065 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm066 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm067 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm070 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm071 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm072 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm073 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm074 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm075 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm076 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm077 = UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0100 = UnixFilePermissions.OwnerExec;
        [AsValue]
        public const UnixFilePermissions Perm0101 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0102 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0103 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0104 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0105 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0106 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0107 = UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0110 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0111 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0112 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0113 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0114 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0115 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0116 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0117 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0120 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0121 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0122 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0123 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0124 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0125 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0126 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0127 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0130 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0131 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0132 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0133 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0134 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0135 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0136 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0137 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0140 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0141 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0142 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0143 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0144 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0145 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0146 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0147 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0150 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0151 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0152 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0153 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0154 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0155 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0156 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0157 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0160 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0161 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0162 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0163 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0164 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0165 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0166 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0167 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0170 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0171 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0172 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0173 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0174 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0175 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0176 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0177 = UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0200 = UnixFilePermissions.OwnerWrite;
        [AsValue]
        public const UnixFilePermissions Perm0201 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0202 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0203 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0204 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0205 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0206 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0207 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0210 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0211 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0212 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0213 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0214 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0215 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0216 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0217 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0220 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0221 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0222 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0223 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0224 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0225 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0226 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0227 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0230 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0231 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0232 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0233 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0234 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0235 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0236 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0237 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0240 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0241 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0242 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0243 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0244 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0245 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0246 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0247 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0250 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0251 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0252 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0253 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0254 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0255 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0256 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0257 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0260 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0261 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0262 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0263 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0264 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0265 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0266 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0267 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0270 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0271 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0272 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0273 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0274 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0275 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0276 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0277 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0300 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec;
        [AsValue]
        public const UnixFilePermissions Perm0301 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0302 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0303 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0304 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0305 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0306 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0307 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0310 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0311 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0312 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0313 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0314 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0315 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0316 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0317 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0320 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0321 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0322 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0323 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0324 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0325 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0326 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0327 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0330 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0331 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0332 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0333 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0334 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0335 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0336 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0337 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0340 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0341 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0342 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0343 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0344 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0345 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0346 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0347 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0350 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0351 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0352 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0353 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0354 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0355 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0356 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0357 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0360 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0361 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0362 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0363 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0364 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0365 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0366 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0367 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0370 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0371 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0372 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0373 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0374 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0375 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0376 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0377 = UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0400 = UnixFilePermissions.OwnerRead;
        [AsValue]
        public const UnixFilePermissions Perm0401 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0402 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0403 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0404 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0405 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0406 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0407 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0410 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0411 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0412 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0413 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0414 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0415 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0416 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0417 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0420 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0421 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0422 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0423 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0424 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0425 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0426 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0427 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0430 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0431 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0432 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0433 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0434 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0435 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0436 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0437 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0440 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0441 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0442 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0443 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0444 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0445 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0446 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0447 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0450 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0451 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0452 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0453 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0454 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0455 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0456 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0457 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0460 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0461 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0462 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0463 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0464 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0465 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0466 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0467 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0470 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0471 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0472 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0473 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0474 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0475 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0476 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0477 = UnixFilePermissions.OwnerRead|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0500 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec;
        [AsValue]
        public const UnixFilePermissions Perm0501 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0502 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0503 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0504 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0505 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0506 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0507 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0510 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0511 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0512 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0513 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0514 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0515 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0516 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0517 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0520 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0521 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0522 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0523 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0524 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0525 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0526 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0527 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0530 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0531 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0532 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0533 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0534 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0535 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0536 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0537 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0540 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0541 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0542 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0543 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0544 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0545 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0546 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0547 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0550 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0551 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0552 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0553 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0554 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0555 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0556 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0557 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0560 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0561 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0562 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0563 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0564 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0565 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0566 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0567 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0570 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0571 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0572 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0573 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0574 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0575 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0576 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0577 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0600 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite;
        [AsValue]
        public const UnixFilePermissions Perm0601 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0602 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0603 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0604 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0605 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0606 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0607 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0610 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0611 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0612 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0613 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0614 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0615 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0616 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0617 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0620 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0621 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0622 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0623 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0624 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0625 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0626 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0627 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0630 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0631 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0632 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0633 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0634 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0635 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0636 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0637 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0640 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0641 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0642 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0643 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0644 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0645 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0646 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0647 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0650 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0651 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0652 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0653 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0654 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0655 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0656 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0657 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0660 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0661 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0662 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0663 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0664 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0665 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0666 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0667 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0670 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0671 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0672 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0673 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0674 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0675 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0676 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0677 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0700 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec;
        [AsValue]
        public const UnixFilePermissions Perm0701 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0702 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0703 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0704 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0705 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0706 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0707 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0710 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0711 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0712 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0713 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0714 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0715 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0716 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0717 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0720 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0721 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0722 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0723 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0724 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0725 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0726 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0727 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0730 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0731 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0732 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0733 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0734 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0735 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0736 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0737 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0740 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead;
        [AsValue]
        public const UnixFilePermissions Perm0741 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0742 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0743 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0744 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0745 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0746 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0747 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0750 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0751 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0752 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0753 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0754 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0755 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0756 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0757 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0760 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite;
        [AsValue]
        public const UnixFilePermissions Perm0761 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0762 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0763 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0764 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0765 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0766 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0767 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0770 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec;
        [AsValue]
        public const UnixFilePermissions Perm0771 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0772 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0773 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0774 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead;
        [AsValue]
        public const UnixFilePermissions Perm0775 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherExec;
        [AsValue]
        public const UnixFilePermissions Perm0776 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite;
        [AsValue]
        public const UnixFilePermissions Perm0777 = UnixFilePermissions.OwnerRead|UnixFilePermissions.OwnerWrite|UnixFilePermissions.OwnerExec|UnixFilePermissions.GroupRead|UnixFilePermissions.GroupWrite|UnixFilePermissions.GroupExec|UnixFilePermissions.OtherRead|UnixFilePermissions.OtherWrite|UnixFilePermissions.OtherExec;    }
    }
