using System;

namespace EInfrastructure.Core.HelpCommon.Serialization.JsonAdapter
{
    /// <summary>
    /// Json 序列化基础类库
    /// </summary>
    public abstract class BaseJsonProvider
    {
        /// <summary>
        /// jason序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public virtual string Serializer(object o, bool format = false)
        {
            return null;
        }

        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object Deserialize(string s, Type type)
        {
            return null;
        }
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
