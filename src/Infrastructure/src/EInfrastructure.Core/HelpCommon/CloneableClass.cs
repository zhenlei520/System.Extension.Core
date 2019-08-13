// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 拷贝类
    /// </summary>
    [Serializable]
    public class CloneableClass : ICloneable
    {
        #region ICloneable 成员

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region 深拷贝

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T DeepClone<T>(T t)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, t);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(objectStream);
            }
        }

        #endregion

        #region 浅拷贝

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ShallowClone<T>()
        {
            return (T) Clone();
        }

        #endregion
    }
}
