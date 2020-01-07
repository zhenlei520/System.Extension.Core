// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;

namespace EInfrastructure.Core.Config.SerializeExtensions.JsonAdapter
{
    internal class DataContractJsonProvider : IJsonProvider
    {
        /// <summary>
        /// 默认格式 UTF8
        /// </summary>
        public Encoding EncodingFormat = Encoding.UTF8;

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region json序列化

        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string Serializer(object o, bool format = false)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(o.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, o);
                return EncodingFormat.GetString(stream.ToArray());
            }
        }

        #endregion

        #region json反序列化

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string s, Type type)
        {
            using (MemoryStream ms = new MemoryStream(EncodingFormat.GetBytes(s)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                return serializer.ReadObject(ms);
            }
        }

        #endregion
    }
}
