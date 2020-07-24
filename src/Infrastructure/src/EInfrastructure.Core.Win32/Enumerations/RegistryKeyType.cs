// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Win32.Enumerations
{
    /// <summary>
    /// 注册表类型
    /// </summary>
    public class RegistryKeyType : Enumeration
    {
        /// <summary>
        /// 注册表基项 HKEY_CLASSES_ROOT
        /// </summary>
        public static RegistryKeyType HKEY_CLASS_ROOT = new RegistryKeyType(1, "HKEY_CLASS_ROOT");

        /// <summary>
        /// 注册表基项 HKEY_CURRENT_USER
        /// </summary>
        public static RegistryKeyType HKEY_CURRENT_USER = new RegistryKeyType(2, "HKEY_CURRENT_USER");

        /// <summary>
        /// 注册表基项 HKEY_LOCAL_MACHINE
        /// </summary>
        public static RegistryKeyType HKEY_LOCAL_MACHINE = new RegistryKeyType(3, "HKEY_LOCAL_MACHINE");

        /// <summary>
        /// 注册表基项 HKEY_USERS
        /// </summary>
        public static RegistryKeyType HKEY_USERS = new RegistryKeyType(4, "HKEY_USERS");

        /// <summary>
        /// 注册表基项 HKEY_CURRENT_CONFIG
        /// </summary>
        public static RegistryKeyType HKEY_CURRENT_CONFIG = new RegistryKeyType(5, "HKEY_CURRENT_CONFIG");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RegistryKeyType(int id, string name) : base(id, name)
        {
        }
    }
}
