// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using EInfrastructure.Core.Configuration.Ioc.Plugs;

namespace EInfrastructure.Core.Serialize.Xml
{
    /// <summary>
    /// XML序列化 部分类
    /// </summary>
    public class XmlProvider : IXmlProvider
    {
        #region 序列化为xml字符串

        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ifNameSpace">是否强制指定命名空间，覆盖默认的命名空间</param>
        /// <param name="nameSpaceDic">默认为null（移除默认命名空间）</param>
        /// <param name="encodingFormat">编码格式(默认utf8)</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string Serializer<T>(T obj, bool ifNameSpace = true,
            Dictionary<string, string> nameSpaceDic = null, Encoding encodingFormat = null)
        {
            if (encodingFormat == null)
            {
                encodingFormat = Encoding.UTF8;
            }

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter vStreamWriter = new StreamWriter(stream, encodingFormat))
                {
                    if (ifNameSpace)
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                        if (nameSpaceDic == null)
                        {
                            ns.Add("", "");
                        }
                        else
                        {
                            foreach (var item in nameSpaceDic)
                            {
                                ns.Add(item.Key, item.Value);
                            }
                        }

                        xs.Serialize(vStreamWriter, obj, ns);
                    }
                    else
                    {
                        xs.Serialize(vStreamWriter, obj);
                    }

                    var r = encodingFormat.GetString(stream.ToArray());
                    return r;
                }
            }
        }

        #endregion

        #region xml字符串反序列化

        /// <summary>
        /// xml字符串反序列化
        /// </summary>
        /// <param name="xml">待反序列化的字符串</param>
        /// <param name="encoding">编码格式，默认utf8</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Deserialize<T>(string xml, Encoding encoding = null)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (MemoryStream stream = new MemoryStream(encoding.GetBytes(xml.ToCharArray())))
            {
                return (T) xmldes.Deserialize(stream);
            }
        }

        #endregion

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            if (method.ReflectedType != null) return method.ReflectedType.Namespace;
            return "EInfrastructure.Core.Serialize.Xml";
        }
    }
}
