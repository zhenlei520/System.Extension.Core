using System;

namespace EInfrastructure.Core.HelpCommon.Serialization.JsonAdapter
{
    /// <summary>
    /// Json 序列化基础类库
    /// </summary>
    public interface IJsonProvider
    {
        /// <summary>
        /// jason序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        string Serializer(object o, bool format = false);

        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Deserialize(string s, Type type);
    }

    /// <summary>
    /// Json序列化方式枚举
    /// </summary>
    public enum EnumJsonMode
    {
        /// <summary>
        /// System.Runtime.Serialization.Json
        /// </summary>
        DataContract,

        /// <summary>
        /// Newtonsoft.Json.dll
        /// </summary>
        Newtonsoft,
    }
}