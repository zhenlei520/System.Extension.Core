// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// NetCommon
    /// </summary>
    public class NetCommon
    {
        #region 获取本机的计算机名

        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string LocalHostName => Dns.GetHostName();

        #endregion

        #region 获取本机的局域网IP地址

        /// <summary>
        /// 获取本机的局域网IP地址
        /// </summary>
        public static List<string> LanIpList => IpList.Where(x => x.IsInnerIp()).ToList();

        #endregion

        #region 获取本机的广域网ip地址

        /// <summary>
        /// 获取本机的局域网IP地址
        /// </summary>
        public static List<string> WanIpList => IpList.Where(x => !x.IsInnerIp()).ToList();

        #endregion

        #region 获取远程客户机的IP地址
        /// <summary>
        /// 获取远程客户机的IP地址
        /// </summary>
        /// <param name="clientSocket">客户端的socket对象</param>
        public static string GetClientIp(Socket clientSocket)
        {
            IPEndPoint client = (IPEndPoint)clientSocket.RemoteEndPoint;
            return client.Address.ToString();
        }
        #endregion

        #region 获取ip地址

        /// <summary>
        /// 获取本机所有IP地址
        /// </summary>
        public static List<string> IpList => GetLocalIpAddress("");

        /// <summary>
        /// 获取本机所有IPv4地址
        /// </summary>
        public static List<string> Ipv4List => GetLocalIpAddress("InterNetwork");

        /// <summary>
        /// 获取本机所有IPv6地址
        /// </summary>
        public static List<string> Ipv6List => GetLocalIpAddress("InterNetworkV6");

        #endregion

        #region private methods

        #region 获取本机所有ip地址

        /// <summary>
        /// 获取本机所有ip地址
        /// </summary>
        /// <param name="netType">"InterNetwork":ipv4地址，"InterNetworkV6":ipv6地址</param>
        /// <returns>ip地址集合</returns>
        private static List<string> GetLocalIpAddress(string netType)
        {
            string hostName = Dns.GetHostName(); //获取主机名称
            IPAddress[] addresses = Dns.GetHostAddresses(hostName); //解析主机IP地址

            List<string> ipList = new List<string>();
            if (netType == string.Empty)
            {
                ipList.AddRange(addresses.Select(t => t.ToString()));
            }
            else
            {
                ipList.AddRange(from t in addresses where t.AddressFamily.ToString() == netType select t.ToString());
            }

            return ipList;
        }

        #endregion

        #endregion
    }
}
