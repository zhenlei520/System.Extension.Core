// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public class DeviceSubType : Enumeration
    {
        /// <summary>
        /// Web
        /// </summary>
        public static DeviceSubType Web = new DeviceSubType(1, "Web");

        /// <summary>
        /// Wap
        /// </summary>
        public static DeviceSubType Wap = new DeviceSubType(2, "Wap");

        /// <summary>
        /// android
        /// </summary>
        public static DeviceSubType Android = new DeviceSubType(3, "Android");

        /// <summary>
        /// IPhone
        /// </summary>
        public static DeviceSubType IPhone = new DeviceSubType(4, "IPhone");

        /// <summary>
        /// ipad
        /// </summary>
        public static DeviceSubType IPad = new DeviceSubType(5, "IPad");

        /// <summary>
        /// Pc
        /// </summary>
        public static DeviceSubType Pc = new DeviceSubType(6, "Pc");

        /// <summary>
        /// Wp
        /// </summary>
        public static DeviceSubType Wp = new DeviceSubType(7, "Wp");


        /// <summary>
        /// 设备类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public DeviceSubType(int id, string name) : base(id, name)
        {
        }
    }
}
