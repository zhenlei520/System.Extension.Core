// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public class DeviceType : Enumeration
    {
        /// <summary>
        /// 电脑
        /// </summary>
        public static DeviceType Pc = new DeviceType(1, "电脑","desktop");

        /// <summary>
        /// 移动手机
        /// </summary>
        public static DeviceType Mobile = new DeviceType(2, "移动手机","mobile");

        /// <summary>
        /// 平板电脑
        /// </summary>
        public static DeviceType Tablet = new DeviceType(3, "平板电脑","tablet");

        /// <summary>
        /// 电视
        /// </summary>
        public static DeviceType Television = new DeviceType(4, "电视","television");

        /// <summary>
        /// 移动设备
        /// IPod Touch
        /// </summary>
        public static DeviceType Media = new DeviceType(5, "移动设备","media");

        /// <summary>
        /// 阅读器
        /// Kindle
        /// </summary>
        public static DeviceType Reader = new DeviceType(6, "阅读器","ereader");

        /// <summary>
        /// 游戏机
        /// </summary>
        public static DeviceType Gaming = new DeviceType(7, "游戏机","gaming");

        /// <summary>
        /// 模拟器
        /// </summary>
        public static DeviceType Emulator = new DeviceType(8, "模拟器","emulator");

        /// <summary>
        /// 未知
        /// </summary>
        public static DeviceType Unknow = new DeviceType(9, "未知","unknow");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="extend"></param>
        public DeviceType(int id, string name,string extend) : base(id, name)
        {
            this.Extend = extend;
        }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string Extend { get; private set; }
    }
}
