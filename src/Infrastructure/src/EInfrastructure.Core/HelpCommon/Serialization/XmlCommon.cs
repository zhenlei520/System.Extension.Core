// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EInfrastructure.Core.HelpCommon.Serialization
{
    /// <summary>
    /// XML序列化 部分类
    /// </summary>
    public class XmlCommon
    {
        #region xml字符串序列化与反序列化

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
        public static string Serializer<T>(T obj, bool ifNameSpace = true,
            Dictionary<string, string> nameSpaceDic = null, Encoding encodingFormat = null)
        {
            if (encodingFormat == null)
            {
                encodingFormat=Encoding.UTF8;
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
        public static T Deserialize<T>(string xml, Encoding encoding = null)
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

        #endregion

        #region 云端xml反序列化为对象

        /// <summary>
        /// 云端xml反序列化为对象
        /// </summary>
        /// <param name="filePath">云端地址</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object ToObjectByDeserializeRemote(string filePath, Type type)
        {
            HttpClient client = new HttpClient();
            Task<Stream> result = client.GetStreamAsync(filePath);
            using (Stream stream = result.Result)
            {
                using (var reader = new StreamReader(stream))
                {
                    var xs = new XmlSerializer(type);
                    try
                    {
                        object obj = xs.Deserialize(reader);
                        reader.Close();
                        return obj;
                    }
                    catch
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        #endregion

        #region 本地文件序列化与反序列化

        #region XML序列化到本地

        /// <summary>
        /// XML序列化到本地
        /// </summary>
        /// <param name="obj">序列对象</param>
        /// <param name="filePath">XML文件路径</param>
        /// <returns>是否成功</returns>
        public static bool SerializeToXml(object obj, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (System.Exception ex)
            {
                throw;
            }
            finally
            {
                fs?.Close();
            }

            return true;
        }

        #endregion

        #region XML反序列化

        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");


            string xml = File.ReadAllText(path, encoding);
            return Deserialize<T>(xml, encoding);
        }

        #endregion

        #endregion
    }
}