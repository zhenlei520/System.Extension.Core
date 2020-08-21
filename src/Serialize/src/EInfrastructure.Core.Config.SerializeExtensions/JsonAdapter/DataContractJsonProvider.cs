// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using EInfrastructure.Core.Configuration.Ioc.Plugs;

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
        /// <param name="action">委托方法</param>
        /// <returns></returns>
        public string Serializer(object o, bool format = false, Func<Exception, string> action = null)
        {
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(o.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, o);
                    return EncodingFormat.GetString(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                if (action != null)
                {
                    return action.Invoke(ex);
                }

                throw new Exception($"json序列化出错,序列化类型：{o.GetType().FullName}");
            }
        }

        #endregion

        #region json反序列化

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public object Deserialize(string str, Type type,  Func<Exception, object> action = null)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(EncodingFormat.GetBytes(str)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                    return serializer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                if (action != null)
                {
                    return action.Invoke(ex);
                }

                throw new Exception($"json反序列化出错,待反序列化的json字符串为：{str}");
            }
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="defaultResult">反序列化异常</param>
        /// <param name="action">委托方法</param>
        /// <returns></returns>
        public T Deserialize<T>(string str, T defaultResult = default(T), Action<Exception> action = null)
            where T : class, new()
        {
            try
            {
                return (T) Deserialize(str, typeof(T));
            }
            catch (Exception ex)
            {
                if (action == null)
                {
                    throw new Exception($"json反序列化出错，反序列化对象默认值为：{new T()},内容：{str}");
                }

                action.Invoke(ex);
                return defaultResult;
            }
        }

        #endregion

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 98;
        }

        #endregion
    }
}
