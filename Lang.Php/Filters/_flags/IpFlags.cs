using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    [Flags]
    public enum IpFlags
    {
        /// <summary>
        /// Allows the IP address to be in IPv4 format.
        /// </summary>
        [RenderValue("FILTER_FLAG_IPV4")]
        IpV4,

        /// <summary>
        /// Allows the IP address to be in IPv6 format.
        /// </summary>
        [RenderValue("FILTER_FLAG_IPV6")]
        IpV6,

        /// <summary>
        /// Fails validation for the following private IPv4 ranges: 
        /// 10.0.0.0/8, 172.16.0.0/12 and 192.168.0.0/16.
        /// Fails validation for the IPv6 addresses starting with FD or FC.
        /// </summary>
        [RenderValue("FILTER_FLAG_NO_PRIV_RANGE")]
        NoPrivRange,

        /// <summary>
        /// Fails validation for the following reserved IPv4 ranges: 
        /// 0.0.0.0/8, 169.254.0.0/16, 192.0.2.0/24 and 224.0.0.0/4. 
        /// This flag does not apply to IPv6 addresses.
        /// </summary>
        [RenderValue("FILTER_FLAG_NO_RES_RANGE")]
        NoResRange
    }
}
