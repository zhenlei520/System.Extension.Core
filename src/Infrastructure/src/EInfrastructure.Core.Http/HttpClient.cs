using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace EInfrastructure.Core.Http
{
    /// <summary>
    /// HttpClient
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        public HttpClient(string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new BusinessException("域名不能为空", HttpStatus.Err.Id);
            }

            Host = host;
            TimeOut = 30000;
            RestClient = new RestClient(Host);
            JsonProvider = new NewtonsoftJsonProvider();
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        public HttpClient(string host, int timeOut) : this(host)
        {
            Host = host;
            TimeOut = timeOut;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        public HttpClient(string host, int timeOut, Encoding encoding) : this(host, timeOut)
        {
            Host = host;
            Encoding = encoding;
        }

        /// <summary>
        ///
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// 超时时间 默认30000s
        /// </summary>
        public int TimeOut { get; private set; }

        public Encoding Encoding { get; private set; }

        /// <summary>
        ///
        /// </summary>
        private RestClient RestClient;

        private NewtonsoftJsonProvider JsonProvider;


        #region Get请求

        #region 同步

        #region 得到响应对象

        #region 响应信息为Json对象

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public T GetFromJson<T>(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            var res = Get(url, headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) JsonProvider.Deserialize(res, typeof(T));
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public T GetFromJson<T>(string url, object data, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            var res = Get(SetUrlParam(url, data), headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) JsonProvider.Deserialize(res, typeof(T));
        }

        #endregion

        #region 响应信息为Xml

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public T GetFromXml<T>(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            var res = Get(url, headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return XmlProvider.Deserialize<T>(res, Encoding);
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public T GetFromXml<T>(string url, object data, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            var res = Get(SetUrlParam(url, data), headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return XmlProvider.Deserialize<T>(res, Encoding);
        }

        #endregion

        #endregion

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public string GetString(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return Get(url, headers, timeOut).Content;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public string GetString(string url, object data, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return Get(SetUrlParam(url, data), headers, timeOut).Content;
        }

        #endregion

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public byte[] GetBytes(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return Get(url, headers, timeOut).RawBytes;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public byte[] GetBytes(string url, object data, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return Get(SetUrlParam(url, data), headers, timeOut).RawBytes;
        }

        #endregion

        #region get请求得到响应流

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public Stream GetStream(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return new MemoryStream(GetBytes(url, headers, timeOut));
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public Stream GetStream(string url, object data, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            return new MemoryStream(GetBytes(SetUrlParam(url, data), headers, timeOut));
        }

        #endregion

        #endregion

        #region 异步

        #region 得到响应对象

        #region 响应信息为Json对象

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<T> GetFromJsonAsync<T>(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            var res = await GetStringAsync(url, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) JsonProvider.Deserialize(res, typeof(T));
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<T> GetFromJsonAsync<T>(string url, object data, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            var res = await GetStringAsync(url, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) JsonProvider.Deserialize(res, typeof(T));
        }

        #endregion

        #region 响应信息为Xml

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<T> GetFromXmlAsync<T>(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            var res = await GetStringAsync(url, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return XmlProvider.Deserialize<T>(res, Encoding);
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<T> GetFromXmlAsync<T>(string url, object data, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            var res = await GetStringAsync(url, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return XmlProvider.Deserialize<T>(res, Encoding);
        }

        #endregion

        #endregion

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return (await GetAsync(url, headers, timeOut)).Content;
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url, object data, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return (await GetAsync(SetUrlParam(url, data), headers, timeOut)).Content;
        }

        #endregion

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return (await GetAsync(url, headers, timeOut)).RawBytes;
        }

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url, object data, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return (await GetAsync(SetUrlParam(url, data), headers, timeOut)).RawBytes;
        }

        #endregion

        #region get请求得到响应流

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return new MemoryStream(await GetBytesAsync(url, headers, timeOut));
        }

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string url, object data, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            return new MemoryStream(await GetBytesAsync(SetUrlParam(url, data), headers, timeOut));
        }

        #endregion

        #endregion

        #region private methods

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        private IRestResponse Get(string url, Dictionary<string, string> headers = null, int? timeOut = null)
        {
            var res = GetAsync(url, headers, timeOut);
            return res.Result;
        }

        /// <summary>
        /// Get请求 异步
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        private async Task<IRestResponse> GetAsync(string url, Dictionary<string, string> headers = null,
            int? timeOut = null)
        {
            RestRequest request = new RestRequest(url, RestSharp.Method.GET) {Timeout = timeOut ?? TimeOut};
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        request.AddHeader(key, headers[key]);
                    }
                }
            }

            return await RestClient.ExecuteTaskAsync(request);
        }

        /// <summary>
        /// 得到url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        private static string SetUrlParam(string url, object paramObj)
        {
            if (paramObj == null)
            {
                return url;
            }

            var type = paramObj.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                string name;
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(JsonProperty)))
                {
                    var namedargument = property.CustomAttributes.Where(x => x.AttributeType == typeof(JsonProperty))
                        .Select(x => x.NamedArguments).FirstOrDefault();
                    name = namedargument.Select(x => x.TypedValue.Value).FirstOrDefault()?.ToString();
                }
                else
                {
                    name = property.Name;
                }

                object valueObj = property.GetValue(paramObj, null);

                if (valueObj == null)
                {
                    continue;
                }

                string value = valueObj.ToString();

                if (!url.Contains("?"))
                {
                    url = url + "?" + name + "=" + value;
                }
                else
                {
                    url = url + "&" + name + "=" + value;
                }
            }

            return url;
        }

        #endregion

        #endregion
    }
}
