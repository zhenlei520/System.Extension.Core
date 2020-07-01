using System.IO;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Exception.Response
{
    /// <summary>
    /// 异常
    /// </summary>
    public class ExceptionResponse<T> : BaseExceptionResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public ExceptionResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">异常码</param>
        /// <param name="content">异常描述</param>
        /// <param name="extend">扩展信息</param>
        public ExceptionResponse(T code, string content, object extend = null) : base(code, content, extend)
        {
        }

        /// <summary>
        /// 异常码
        /// </summary>
        [JsonIgnore]
        public T GetCode => Code;

        /// <summary>
        /// 异常响应内容
        /// </summary>
        [JsonIgnore]
        public string GetContent => Content;

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonIgnore]
        public object GetExtend => Extend;
    }

    /// <summary>
    /// 异常
    /// </summary>
    public abstract class BaseExceptionResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public BaseExceptionResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">异常码</param>
        /// <param name="content">异常描述</param>
        /// <param name="extend">扩展信息</param>
        public BaseExceptionResponse(T code, string content, object extend = null)
        {
            Code = code;
            Content = content;
            Extend = extend;
        }

        /// <summary>
        /// 异常码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        internal T Code { get; set; }

        /// <summary>
        /// 异常响应内容
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        internal string Content { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "extend", DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal object Extend { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Serializer(this);
        }

        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string Serializer(object o)
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
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                using (jsonWriter)
                {
                    serializer.Serialize(jsonWriter, o);
                }

                return sw.ToString();
            }
        }
    }
}
