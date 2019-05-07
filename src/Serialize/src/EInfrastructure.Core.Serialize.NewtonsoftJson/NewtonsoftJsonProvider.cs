// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Serialize.NewtonsoftJson
{
    public class NewtonsoftJsonProvider : IJsonProvider
    {
        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
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
            return JsonConvert.DeserializeObject(s, type);
        }

        #endregion
    }
}
