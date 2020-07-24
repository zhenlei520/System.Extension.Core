// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Win32.Enumerations;
using Microsoft.Win32;

namespace EInfrastructure.Core.Win32
{
    /// <summary>
    ///
    /// </summary>
    public class RegistryCommon
    {
        /// <summary>
        /// 默认注册表基项
        /// </summary>
        private string baseKey = "Software";

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseKey">基项的名称</param>
        public RegistryCommon()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseKey">基项的名称</param>
        public RegistryCommon(string baseKey)
        {
            this.baseKey = baseKey;
        }

        #endregion

        #region 公共方法

        #region 写入注册表,如果指定项已经存在,则修改指定项的值

        /// <summary>
        /// 写入注册表,如果指定项已经存在,则修改指定项的值
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表项,不包括基项</param>
        /// <param name="name">值名称</param>
        /// <param name="values">值</param>
        public void SetValue(RegistryKeyType keytype, string key, string name, string values)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey, true);
            RegistryKey rkt = software.CreateSubKey(key);
            if (rkt != null)
            {
                rkt.SetValue(name, values);
            }
        }

        #endregion

        #region 读取注册表

        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表项,不包括基项</param>
        /// <param name="name">值名称</param>
        /// <returns>返回字符串</returns>
        public string GetValue(RegistryKeyType keytype, string key, string name)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey, true);
            RegistryKey rkt = software.OpenSubKey(key);

            if (rkt != null)
            {
                return rkt.GetValue(name).ToString();
            }

            return string.Empty;
        }

        #endregion

        #region 删除注册表中的值

        /// <summary>
        /// 删除注册表中的值
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表项名称,不包括基项</param>
        /// <param name="name">值名称</param>
        public void DeleteValue(RegistryKeyType keytype, string key, string name)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey, true);
            RegistryKey rkt = software.OpenSubKey(key, true);

            if (rkt != null)
            {
                object value = rkt.GetValue(name);
                if (value != null)
                {
                    rkt.DeleteValue(name, true);
                }
            }
        }

        #endregion

        #region 删除注册表中的指定项

        /// <summary>
        /// 删除注册表中的指定项
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表中的项,不包括基项</param>
        /// <returns>返回布尔值,指定操作是否成功</returns>
        public void DeleteSubKey(RegistryKeyType keytype, string key)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey, true);
            if (software != null)
            {
                software.DeleteSubKeyTree(key);
            }
        }

        #endregion

        #region 判断指定项是否存在

        /// <summary>
        /// 判断指定项是否存在
        /// </summary>
        /// <param name="keytype">基项枚举</param>
        /// <param name="key">指定项字符串</param>
        /// <returns>返回布尔值,说明指定项是否存在</returns>
        public bool IsExist(RegistryKeyType keytype, string key)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey);
            RegistryKey rkt = software.OpenSubKey(key);
            if (rkt != null)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region 检索指定项关联的所有值

        /// <summary>
        /// 检索指定项关联的所有值
        /// </summary>
        /// <param name="keytype">基项枚举</param>
        /// <param name="key">指定项字符串</param>
        /// <returns>返回指定项关联的所有值的字符串数组</returns>
        public string[] GetValues(RegistryKeyType keytype, string key)
        {
            RegistryKey rk = (RegistryKey) GetRegistryKey(keytype);
            RegistryKey software = rk.OpenSubKey(baseKey, true);
            RegistryKey rkt = software.OpenSubKey(key);
            string[] names = rkt.GetValueNames();

            if (names.Length == 0)
            {
                return names;
            }
            else
            {
                string[] values = new string[names.Length];

                int i = 0;

                foreach (string name in names)
                {
                    values[i] = rkt.GetValue(name).ToString();

                    i++;
                }

                return values;
            }
        }

        #endregion

        #region 将对象所有属性写入指定注册表中

        /// <summary>
        /// 将对象所有属性写入指定注册表中
        /// </summary>
        /// <param name="keyType">注册表基项枚举</param>
        /// <param name="key">注册表项,不包括基项</param>
        /// <param name="obj">传入的对象</param>
        public void SetObjectValue(RegistryKeyType keyType, string key, Object obj)
        {
            if (obj != null)
            {
                Type t = obj.GetType();

                string name;
                object value;
                foreach (var p in t.GetProperties())
                {
                    if (p != null)
                    {
                        name = p.Name;
                        value = p.GetValue(obj, null);
                        this.SetValue(keyType, key, name, value.ToString());
                    }
                }
            }
        }

        #endregion

        #endregion

        #region private metods

        #region 返回RegistryKey对象

        /// <summary>
        /// 返回RegistryKey对象
        /// </summary>
        /// <param name="keyType">注册表基项枚举</param>
        /// <returns></returns>
        private object GetRegistryKey(RegistryKeyType keyType)
        {
            if (keyType.Equals(RegistryKeyType.HKEY_CLASS_ROOT))
            {
                return Registry.ClassesRoot;
            }

            if (keyType.Equals(RegistryKeyType.HKEY_CURRENT_USER))
            {
                return Registry.CurrentUser;
            }

            if (keyType.Equals(RegistryKeyType.HKEY_LOCAL_MACHINE))
            {
                return Registry.LocalMachine;
            }

            if (keyType.Equals(RegistryKeyType.HKEY_USERS))
            {
                return Registry.Users;
            }

            if (keyType.Equals(RegistryKeyType.HKEY_CURRENT_CONFIG))
            {
                return Registry.CurrentConfig;
            }

            return null;
        }

        #endregion

        #endregion
    }
}
