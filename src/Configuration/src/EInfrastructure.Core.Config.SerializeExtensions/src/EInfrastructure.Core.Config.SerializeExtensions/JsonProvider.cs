// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Config.SerializeExtensions
{
    /// <summary>
    ///
    /// </summary>
    public class JsonProvider : IDependency
    {
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode">序列化方式</param>
        public JsonProvider(ICollection<IJsonProvider> jsonProviders = null)
        {
            if (jsonProviders == null)
            {
                throw new Exception("未找到相应的json序列化Provider");
            }

            if (jsonProviders.Any(x => x.GetIdentify().Contains("NewtonsoftJson")))
            {
                _jsonProvider = jsonProviders.FirstOrDefault(x => x.GetIdentify().Contains("NewtonsoftJson"));
            }
            else
            {
                _jsonProvider = jsonProviders.FirstOrDefault();
            }

            if (_jsonProvider == null)
            {
                throw new Exception("未找到相应的json序列化Provider");
            }
        }

        #region json序列化

        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string Serializer(object o, bool format = false)
        {
            try
            {
                return _jsonProvider.Serializer(o, format);
            }
            catch (System.Exception)
            {
                throw new System.Exception($"json序列化出错,jsonMode:{1},序列化类型：{o.GetType().FullName}");
            }
        }

        #endregion

        #region json反序列化

        /// <summary>
        /// json反序列化
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
                return (T) _jsonProvider.Deserialize(s, typeof(T));
            }
            catch (System.Exception ex)
            {
                if (action == null)
                {
                    throw new System.Exception($"json反序列化出错,jsonMode:{1},内容：{s}");
                }

                action.Invoke(ex);
                return defaultResult;
            }
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string s, Type type)
        {
            try
            {
                return _jsonProvider.Deserialize(s, type);
            }
            catch (System.Exception)
            {
                throw new System.Exception($"json反序列化出错,jsonMode:{1},内容：{s}");
            }
        }

        #endregion
    }
}
