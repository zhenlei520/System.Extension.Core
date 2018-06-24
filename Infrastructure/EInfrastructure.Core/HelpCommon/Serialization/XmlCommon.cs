using System;
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
        public static Encoding EncodingFormat = Encoding.GetEncoding("gbk");

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public static string Serializer<T>(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("", "");
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter vStreamWriter = new StreamWriter(stream, EncodingFormat))
                {
                    xs.Serialize(vStreamWriter, obj, ns);
                    var r = EncodingFormat.GetString(stream.ToArray());
                    return r;
                }
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public static string Serializer<T>(T obj, bool ifNameSpace)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter vStreamWriter = new StreamWriter(stream, EncodingFormat))
                {
                    if (ifNameSpace == false)
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
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

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(EncodingFormat.GetBytes(xml)))
            {
                var r = (T)xs.Deserialize(stream);
                return r;
            }
        }

        #region 序列化为xml文件(保存到本地)
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

        #region 本地xml文件反序列化
        /// <summary>
        /// 本地xml文件反序列化
        /// </summary>
        /// <param name="filePath">文件绝对地址（服务器端地址）</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object ToObjectByDeserializeByFile(string filePath, Type type)
        {
            if (!File.Exists(filePath))
                return null;
            using (var reader = new StreamReader(filePath))
            {
                var xs = new XmlSerializer(type);
                object obj = xs.Deserialize(reader);
                reader.Close();
                return obj;
            }
        }

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

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <param name="type">目标类型（Type类型）</param>
        /// <param name="filePath">XML文件路径</param>
        /// <returns>序列对象</returns>
        public static object DeserializeFromXml(Type type, string filePath)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (System.Exception ex)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion
    }
}
