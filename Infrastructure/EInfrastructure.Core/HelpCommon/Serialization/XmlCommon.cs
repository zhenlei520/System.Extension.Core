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
    public class XmlHelper
    {
        /// <summary>
        /// xml编码格式
        /// </summary>
        public static Encoding EncodingFormat = Encoding.Default;

        #region 序列化为xml字符串

        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ifNameSpace">是否强制指定命名空间，覆盖默认的命名空间</param>
        /// <param name="nameSpaceDic">默认为null（移除默认命名空间）</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string Serializer<T>(T obj, bool ifNameSpace = true,
            Dictionary<string, string> nameSpaceDic = null)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter vStreamWriter = new StreamWriter(stream, EncodingFormat))
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

                    var r = EncodingFormat.GetString(stream.ToArray());
                    return r;
                }
            }
        }

        #endregion

        #region xml字符串反序列化

        /// <summary>
        /// xml字符串反序列化
        /// </summary>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml.ToCharArray())))
            {
                return (T) xmldes.Deserialize(stream);
            }
        }

        #endregion

        #region 序列化与反序列化 加保存

        #region 序列化为xml文件(且保存到本地)

        /// <summary>
        /// 序列化为xml(保存到本地)
        /// </summary>
        /// <param name="filePath">本地文件路径，绝对路径（服务器）</param>
        /// <param name="obj">对象</param>
        public static bool ToSaveXmlBySerializer(object obj, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return true;
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

        #region XML序列化

        /// <summary>
        /// XML序列化
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

        

        #endregion

        #endregion
    }
}