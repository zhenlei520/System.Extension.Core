// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Serialize.NewtonsoftJson
{
    /// <summary>
    ///
    /// </summary>
    public class NewtonsoftJsonProvider : IJsonProvider
    {
        /// <summary>
        ///
        /// </summary>
        public NewtonsoftJsonProvider()
        {
        }

        private readonly ILogProvider _logService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="providers"></param>
        public NewtonsoftJsonProvider(ICollection<ILogProvider> providers) : this()
        {
            _logService = providers.Count == 1
                ? providers.FirstOrDefault()
                : providers.OrderByDescending(x => x.GetWeights()).FirstOrDefault();
        }

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
        /// <param name="action"></param>
        /// <returns></returns>
        public string Serializer(object o, bool format = false, Func<Exception, string> action = null)
        {
            try
            {
                if (o == null)
                {
                    return string.Empty;
                }

                using (StringWriter sw = new StringWriter())
                {
                    JsonSerializer serializer = JsonSerializer.Create(
                        new JsonSerializerSettings
                        {
                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
                    );
                    serializer.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    JsonWriter jsonWriter;
                    if (format)
                    {
                        jsonWriter = new JsonTextWriter(sw)
                        {
                            Formatting = Formatting.Indented,
                            Indentation = 4,
                            IndentChar = ' '
                        };
                    }
                    else
                    {
                        jsonWriter = new JsonTextWriter(sw);
                    }

                    using (jsonWriter)
                    {
                        serializer.Serialize(jsonWriter, o);
                    }

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                if (action != null)
                {
                    return action.Invoke(ex);
                }

                throw new System.Exception($"json序列化出错,序列化类型：{o.GetType().FullName}");
            }
        }

        #endregion

        #region json反序列化

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <param name="action">委托方法</param>
        /// <returns></returns>
        public object Deserialize(string str, Type type, Func<Exception, object> action = null)
        {
            try
            {
                return JsonConvert.DeserializeObject(str, type);
            }

            catch (System.Exception ex)
            {
                if (StartUp.EnableLog)
                {
                    _logService?.Info($"反序列化失败，待转字符串str：{str}" + "，异常信息：" + ex.Message);
                }

                if (action != null)
                {
                    return action.Invoke(ex);
                }

                throw;
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
                object obj = Deserialize(str, typeof(T), (exception =>
                {
                    action?.Invoke(exception);
                    return defaultResult;
                }));
                return (T) obj;
            }
            catch (Exception ex)
            {
                action?.Invoke(ex);
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
            return 99;
        }

        #endregion
    }
}
