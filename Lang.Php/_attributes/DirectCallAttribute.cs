using System;
using System.Linq;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class DirectCallAttribute : Attribute
    {
		#region Constructors 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name in script</param>
        public DirectCallAttribute(string name)
        {
            this.Name = name;
            OutNr = int.MinValue;
        }

        public DirectCallAttribute(string name, string map, int outNr = int.MinValue)
        {
            this.Name = name;
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
