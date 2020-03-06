using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.Http.Params;
using EInfrastructure.Core.Http.Provider;
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
            _restClient = new RestClient(Host);
            _jsonProvider = new NewtonsoftJsonProvider();
            _xmlProvider = new XmlProvider();
            _encoding = Encoding.UTF8;
            _files = new List<RequestMultDataParam>();
            _requestBodyType = RequestBodyType.ApplicationJson;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="jsonProvider"></param>
        /// <param name="requestBodyType"></param>
        public HttpClient(string host, int timeOut, IJsonProvider jsonProvider = null,
            RequestBodyType requestBodyType = null) : this(host)
        {
            Host = host;
            TimeOut = timeOut;
            _jsonProvider = jsonProvider ?? new NewtonsoftJsonProvider();
            _requestBodyType = requestBodyType ?? RequestBodyType.ApplicationJson;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="xmlProvider"></param>
        /// <param name="requestBodyType"></param>
        public HttpClient(string host, int timeOut, IXmlProvider xmlProvider = null,
            RequestBodyType requestBodyType = null) : this(host)
        {
            Host = host;
            TimeOut = timeOut;
            _xmlProvider = xmlProvider ?? new XmlProvider();
            _requestBodyType = requestBodyType ?? RequestBodyType.ApplicationJson;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="encoding">编码格式 默认Utf8</param>
        /// <param name="jsonProvider"></param>
        public HttpClient(string host, int timeOut, Encoding encoding, IJsonProvider jsonProvider = null) : this(host,
            timeOut, jsonProvider ?? new NewtonsoftJsonProvider())
        {
            Host = host;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="encoding">编码格式 默认Utf8</param>
        /// <param name="xmlProvider"></param>
        public HttpClient(string host, int timeOut, Encoding encoding, IXmlProvider xmlProvider = null) : this(host,
            timeOut, xmlProvider ?? new XmlProvider())
        {
            Host = host;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        IProvider GetProvider()
        {
            if (_requestBodyType.Id == RequestBodyType.ApplicationJson.Id)
            {
                return new PostByApplicationJsonProvider();
            }

            if (_requestBodyType.Id == RequestBodyType.ApplicationXWwwFormUrlencoded.Id)
            {
                return new PostByApplicationXWwwFormUrlencodedProvider();
            }

            if (_requestBodyType.Id == RequestBodyType.MultipartFormData.Id)
            {
                return new PostByMultipartFormDataProvider();
            }

            if (_requestBodyType.Id == RequestBodyType.Text.Id)
            {
                return new PostByTextProvider();
            }

            if (_requestBodyType.Id == RequestBodyType.TextXml.Id)
            {
                return new PostByTextXmlProvider();
            }

            throw new BusinessException("不支持的请求");
        }

        /// <summary>
        ///域
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// 超时时间 默认30000s
        /// </summary>
        public int TimeOut { get; }

        /// <summary>
        /// 编码格式
        /// </summary>
        private Encoding _encoding;

        /// <summary>
        ///
        /// </summary>
        private RestClient _restClient;

        /// <summary>
        ///
        /// </summary>
        private IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        private IXmlProvider _xmlProvider;

        /// <summary>
        /// 文件信息
        /// </summary>
        private List<RequestMultDataParam> _files;

        /// <summary>
        /// body请求类型
        /// </summary>
        private readonly RequestBodyType _requestBodyType;

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

            return (T) _jsonProvider.Deserialize(res, typeof(T));
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
            var res = Get(SetUrlParam(url, GetParams(data)), headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) _jsonProvider.Deserialize(res, typeof(T));
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

            return _xmlProvider.Deserialize<T>(res, _encoding);
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
            var res = Get(SetUrlParam(url, GetParams(data)), headers, timeOut).Content;
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
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
            return Get(SetUrlParam(url, GetParams(data)), headers, timeOut).Content;
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
            return Get(SetUrlParam(url, GetParams(data)), headers, timeOut).RawBytes;
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
            return new MemoryStream(GetBytes(SetUrlParam(url, GetParams(data)), headers, timeOut));
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

            return (T) _jsonProvider.Deserialize(res, typeof(T));
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
            var res = await GetStringAsync(url, data, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) _jsonProvider.Deserialize(res, typeof(T));
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

            return _xmlProvider.Deserialize<T>(res, _encoding);
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
            var res = await GetStringAsync(url, data, headers, timeOut);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
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
            return (await GetAsync(SetUrlParam(url, GetParams(data)), headers, timeOut)).Content;
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
            return (await GetAsync(SetUrlParam(url, GetParams(data)), headers, timeOut)).RawBytes;
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
            return new MemoryStream(await GetBytesAsync(SetUrlParam(url, GetParams(data)), headers, timeOut));
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
            RestRequest request = GetProvider()
                .GetRequest(Method.GET, url, new RequestBody(null), headers, timeOut ?? TimeOut);
            return await _restClient.ExecuteTaskAsync(request);
        }

        /// <summary>
        /// 得到url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static string SetUrlParam(string url, Dictionary<string, string> properties)
        {
            foreach (var property in properties)
            {
                string value = property.Value ?? "";

                if (!url.Contains("?"))
                {
                    url = url + "?" + property.Key + "=" + value;
                }
                else
                {
                    url = url + "&" + property.Key + "=" + value;
                }
            }

            return url;
        }

        #endregion

        #endregion

        #region Post请求

        #region 同步

        #region Post请求得到byte数组

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 可通过EName 属性为参数重命名</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public byte[] PostByBytes(string url, object data, Dictionary<string, string> headers = null,
            RequestBodyFormat requestBodyFormat = null, int? timeOut = null)
        {
            return Post(url, data, headers, requestBodyFormat, timeOut).RawBytes;
        }

        #endregion

        #region Post请求得到响应流

        /// <summary>
        /// Post请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data"></param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="requestBodyFormat"></param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public Stream PostByStream(string url, object data, Dictionary<string, string> headers = null,
            RequestBodyFormat requestBodyFormat = null, int? timeOut = null)
        {
            return new MemoryStream(PostByBytes(url, data, headers, requestBodyFormat, timeOut));
        }

        #endregion

        #endregion

        #region private methods

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求对象</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        private IRestResponse Post(string url, object data, Dictionary<string, string> headers = null,
            RequestBodyFormat requestBodyFormat = null,
            int? timeOut = null)
        {
            var res = PostAsync(url, data, headers, requestBodyFormat, timeOut);
            return res.Result;
        }

        /// <summary>
        /// Get请求 异步
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求文本对象</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        private async Task<IRestResponse> PostAsync(string url, object data,
            Dictionary<string, string> headers = null,
            RequestBodyFormat requestBodyFormat = null,
            int? timeOut = null)
        {
            var body = data;
            if (_requestBodyType.Id == RequestBodyType.TextXml.Id)
            {
                body = _xmlProvider.Serializer(data);
            }

            var request = GetProvider().GetRequest(Method.POST, url, new RequestBody(body, requestBodyFormat, _files),
                headers, timeOut ?? TimeOut);
            return await _restClient.ExecuteTaskAsync(request);
        }

        #endregion

        #endregion

        #region private methods

        #region 得到参数

        /// <summary>
        /// 得到参数
        /// </summary>
        /// <param name="data">对象 允许自定义参数名，可以从JsonProperty的属性中获取</param>
        /// <returns></returns>
        private Dictionary<string, string> GetParams(object data)
        {
            if (data == null || data is string || !data.GetType().IsClass)
            {
                return new Dictionary<string, string>();
            }

            var type = data.GetType();
            var properties = type.GetProperties();

            Dictionary<string, string> objectDic = new Dictionary<string, string>();
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

                if (objectDic.All(x => x.Key != name) && name != null)
                {
                    objectDic.Add(name, property.GetValue(data, null)?.ToString() ?? "");
                }
            }

            return objectDic;
        }

        #endregion

        #endregion
    }
}
