// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.HelpCommon.Serialization.JsonAdapter;

namespace EInfrastructure.Core.HelpCommon.Serialization
{
    /// <summary>
    /// json 序列化方式
    /// </summary>
    public class JsonCommon : IJsonProvider
    {
        private readonly EnumJsonMode _jsonMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">序列化方式</param>
        public JsonCommon(EnumJsonMode mode = EnumJsonMode.Newtonsoft)
        {
            _jsonMode = mode;
        }

        private IJsonProvider CreateJsonProvider()
        {
            if (_jsonMode == EnumJsonMode.Newtonsoft)
            {
                return new NewtonsoftJsonProvider();
            }
            else if (_jsonMode == EnumJsonMode.DataContract)
            {
                return new DataContractJsonProvider();
            }

            throw new System.Exception("未找到相应的json序列化Provider");
        }

        /// <summary>
        /// jason序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string Serializer(object o, bool format = false)
        {
            try
            {
                return CreateJsonProvider().Serializer(o, format);
            }
            catch (System.Exception)
            {
                throw new System.Exception($"json序列化出错,序列化类型：{o.GetType().FullName}");
            }
        }

        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="defaultResult">反序列化异常</param>
        /// <param name="action">委托方法</param>
        /// <returns></returns>
        public T Deserialize<T>(string s, T defaultResult = default(T), Action<System.Exception> action = null)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return defaultResult;
            }

            try
            {
                return (T) CreateJsonProvider().Deserialize(s, typeof(T));
            }
            catch (System.Exception ex)
            {
                if (action == null)
                {
                    throw new System.Exception($"json反序列化出错,待序列化的json字符串为：{s}");
                }

                action.Invoke(ex);
                return defaultResult;
            }
        }

        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string s, Type type)
        {
            try
            {
                return CreateJsonProvider().Deserialize(s, type);
            }
            catch (System.Exception)
            {
                throw new System.Exception($"json反序列化出错,待序列化的json字符串为：{s}");
            }
        }
    }
}