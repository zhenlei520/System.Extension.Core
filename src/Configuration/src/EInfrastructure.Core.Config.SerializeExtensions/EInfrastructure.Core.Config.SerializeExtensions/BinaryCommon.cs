// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EInfrastructure.Core.Config.SerializeExtensions
{
    /// <summary>
    /// 二进制序列化
    /// </summary>
    public class BinaryCommon
    {
        #region 序列化对象(二进制)

        /// <summary>
        /// 序列化对象(二进制)
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        public static byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        #endregion

        #region 反序列化对象(二进制)

        /// <summary>
        /// 反序列化对象(二进制)
        /// </summary>
        /// <param name="bytes">需要反序列化的字符串/></param>
        public static object Deserialize(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(ms);
            }
        }

        #endregion
    }
}
