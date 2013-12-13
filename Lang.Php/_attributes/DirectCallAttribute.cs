using System;
using System.Linq;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class DirectCallAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name in script</param>
        public DirectCallAttribute(string name)
        {
            SetName(name);
            OutNr = int.MinValue;
        }

        private void SetName(string name)
        {
            name = name
                .Replace("->", " -> ")
                .Replace("$", " $ ");
            var tmp = name.Split(' ').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).ToArray();
            this.Name = tmp.Any() ? tmp.Last() : null;
            tmp = tmp.Take(tmp.Length - 1).Select(i => i.ToLower()).ToArray();
            CallType = MethodCallStyles.Procedural;
            if (tmp.Contains("->") || tmp.Contains("instance"))
                CallType = MethodCallStyles.Instance;
            else if (tmp.Contains("::") || tmp.Contains("static"))
                CallType = MethodCallStyles.Static;

            MemberToCall = ClassMembers.Method;
            if (tmp.Contains("field") || tmp.Contains("$"))
                MemberToCall = ClassMembers.Field;

        }
        public MethodCallStyles CallType { get; private set; }
        public ClassMembers MemberToCall { get; private set; }


        public DirectCallAttribute(string name, string map, int outNr = int.MinValue)
        {
            SetName(name);
            this.Map = (map ?? "").Trim();
            this.OutNr = outNr;
        }

        #endregion Constructors

        #region Fields

        public const int THIS = int.MinValue;

        #endregion Fields

        #region Properties




        public bool HasMapping
        {
            get
            {
                return !string.IsNullOrEmpty(Map);
            }
        }

        /// <summary>
        /// Argument map i.e.   0,1,3,this
        /// </summary>
        public string Map { get; private set; }

        public int OutNr { get; private set; }

        public int[] MapArray
        {
            get
            {
                if (string.IsNullOrEmpty(Map))
                    return null;
                return (
                    from item in Map.Split(',')
                    let tmp = item.Trim().ToLower()
                    select tmp == "this" ? THIS : int.Parse(tmp)
                    ).ToArray();
            }

        }

        /// <summary>
        /// Name in script
        /// </summary>
        public string Name { get; set; }

        #endregion Properties
    }
}
