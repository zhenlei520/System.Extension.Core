using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EInfrastructure.Core.HelpCommon.Serialization.JsonAdapter
{
    internal class DataContractJsonProvider : IJsonProvider
    {
        /// <summary>
        /// 默认格式 UTF8
        /// </summary>
        public Encoding EncodingFormat = Encoding.UTF8;

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
    }
}
