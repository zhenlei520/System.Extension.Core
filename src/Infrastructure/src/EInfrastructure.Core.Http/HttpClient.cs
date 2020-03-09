using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
            _restClient = new RestClient(Host);
            _jsonProvider = new NewtonsoftJsonProvider();
            _xmlProvider = new XmlProvider();
            _encoding = Encoding.UTF8;
            _files = new List<RequestMultDataParam>();
            Headers = new Dictionary<string, string>();
            _requestBodyType = RequestBodyType.ApplicationJson;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="jsonProvider"></param>
        /// <param name="requestBodyType"></param>
        public HttpClient(string host, IJsonProvider jsonProvider = null,
            RequestBodyType requestBodyType = null) : this(host)
        {
            Host = host;
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
        public HttpClient(string host, IXmlProvider xmlProvider = null,
            RequestBodyType requestBodyType = null) : this(host)
        {
            Host = host;
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
        public HttpClient(string host, Encoding encoding, IJsonProvider jsonProvider = null) : this(host,
            jsonProvider ?? new NewtonsoftJsonProvider())
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
            xmlProvider ?? new XmlProvider())
        {
            Host = host;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        IProvider GetProvider(RequestType requestType)
        {
            if (requestType.Id == RequestType.Get.Id)
            {
                return new GetProvider();
            }
            else if (requestType.Id == RequestType.Post.Id)
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
            }

            throw new BusinessException("不支持的请求");
        }

        /// <summary>
        ///域
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// 超时时间 默认30000 ms
        /// </summary>
        public int? TimeOut;

        /// <summary>
        /// 编码格式
        /// </summary>
        private readonly Encoding _encoding;

        /// <summary>
        ///
        /// </summary>
        private readonly RestClient _restClient;

        /// <summary>
        ///
        /// </summary>
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        private readonly IXmlProvider _xmlProvider;

        /// <summary>
        /// 文件信息
        /// </summary>
        private List<RequestMultDataParam> _files;

        /// <summary>
        /// body请求类型
        /// </summary>
        private readonly RequestBodyType _requestBodyType;

        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string, string> Headers;

        /// <summary>
        /// 代理
        /// </summary>
        public WebProxy Proxy;

        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent;

        /// <summary>
        /// 是否启用Https
        /// </summary>
        public bool IsHttps;

        #region Get请求

        #region 同步

        #region 得到响应对象

        #region 响应信息为Json对象

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public T GetJson<T>(string url)
        {
            var res = Get(url).Content;
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
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public T GetJson<T>(string url, object data)
        {
            var res = Get(SetUrlParam(url, GetParams(data))).Content;
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
        /// <returns></returns>
        public T GetXml<T>(string url)
        {
            var res = Get(url).Content;
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
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public T GetXml<T>(string url, object data)
        {
            var res = Get(SetUrlParam(url, GetParams(data))).Content;
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
        /// <param name="timeOut">超时时间，不设置的话默认与当前配置一致</param>
        /// <returns></returns>
        public string GetString(string url, int? timeOut = null)
        {
            return Get(url).Content;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public string GetString(string url, object data)
        {
            return Get(SetUrlParam(url, GetParams(data))).Content;
        }

        #endregion

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public byte[] GetBytes(string url)
        {
            return Get(url).RawBytes;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public byte[] GetBytes(string url, object data)
        {
            return Get(SetUrlParam(url, GetParams(data))).RawBytes;
        }

        #endregion

        #region get请求得到响应流

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public Stream GetStream(string url)
        {
            return new MemoryStream(GetBytes(url));
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public Stream GetStream(string url, object data)
        {
            return new MemoryStream(GetBytes(SetUrlParam(url, GetParams(data))));
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
        /// <returns></returns>
        public async Task<T> GetJsonAsync<T>(string url)
        {
            var res = await GetStringAsync(url, GetHeaders());
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
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<T> GetFromJsonAsync<T>(string url, object data)
        {
            var res = await GetStringAsync(url, data);
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
        /// <returns></returns>
        public async Task<T> GetXmlAsync<T>(string url)
        {
            var res = await GetStringAsync(url);
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
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<T> GetFromXmlAsync<T>(string url, object data)
        {
            var res = await GetStringAsync(url, data);
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
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url)
        {
            return (await GetAsync(url)).Content;
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url, object data)
        {
            return (await GetAsync(SetUrlParam(url, GetParams(data)))).Content;
        }

        #endregion

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url)
        {
            return (await GetAsync(url)).RawBytes;
        }

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url, object data)
        {
            return (await GetAsync(SetUrlParam(url, GetParams(data)))).RawBytes;
        }

        #endregion

        #region get请求得到响应流

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string url)
        {
            return new MemoryStream(await GetBytesAsync(url));
        }

        /// <summary>
        /// get请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string url, object data)
        {
            return new MemoryStream(await GetBytesAsync(SetUrlParam(url, GetParams(data))));
        }

        #endregion

        #endregion

        #region private methods

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        private IRestResponse Get(string url)
        {
            var res = GetAsync(url);
            return res.Result;
        }

        /// <summary>
        /// Get请求 异步
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        private async Task<IRestResponse> GetAsync(string url)
        {
            RestRequest request = GetProvider(RequestType.Get)
                .GetRequest(Method.GET, url, new RequestBody(null), GetHeaders(), GetTimeOut());
            if (Proxy != null)
            {
                _restClient.Proxy = Proxy;
            }

            return await GetClient().ExecuteTaskAsync(request);
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

        #region Post请求得到响应信息为Json对象

        /// <summary>
        /// Post请求得到响应信息为Json对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetJsonByPost<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = GetStringByPost(url, data, requestBodyFormat);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return (T) _jsonProvider.Deserialize(res, typeof(T));
        }

        #endregion

        #region Post请求得到响应信息为Xml对象

        /// <summary>
        /// Post请求得到响应信息为Xml对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 </param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetXmlByPost<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = GetStringByPost(url, data, requestBodyFormat);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
        }

        #endregion

        #region Post请求得到响应内容

        /// <summary>
        /// Post请求得到响应内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        public string GetStringByPost(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            return GetByPost(url, data, requestBodyFormat).Content;
        }

        #endregion

        #region Post请求得到byte数组

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        public byte[] GetBytesByPost(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            return GetByPost(url, data, requestBodyFormat).RawBytes;
        }

        #endregion

        #region Post请求得到响应流

        /// <summary>
        /// Post请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data"></param>
        /// <param name="requestBodyFormat"></param>
        /// <returns></returns>
        public Stream GetStreamByPost(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            return new MemoryStream(GetBytesByPost(url, data, requestBodyFormat));
        }

        #endregion

        #endregion

        #region 异步请求

        #region Post请求得到响应信息为Json对象

        /// <summary>
        /// Post请求得到响应信息为Json对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> PostByJsonAsync<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await PostByStringAsync(url, data, requestBodyFormat);
            if (string.IsNullOrEmpty(res))
            {
                return default;
            }

            return (T) _jsonProvider.Deserialize(res, typeof(T));
        }

        #endregion

        #region Post请求得到响应信息为Xml对象

        /// <summary>
        /// Post请求得到响应信息为Xml对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数 </param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> PostByXmlAsync<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await PostByStringAsync(url, data, requestBodyFormat);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
        }

        #endregion

        #region Post请求得到响应内容

        /// <summary>
        /// Post请求得到响应内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        public async Task<string> PostByStringAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await GetByPostAsync(url, data, requestBodyFormat);
            return res.Content;
        }

        #endregion

        #region Post请求得到byte数组

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        public async Task<byte[]> PostByBytesAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await GetByPostAsync(url, data, requestBodyFormat);
            return res.RawBytes;
        }

        #endregion

        #region Post请求得到响应流

        /// <summary>
        /// Post请求得到响应流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data"></param>
        /// <param name="requestBodyFormat"></param>
        /// <returns></returns>
        public async Task<Stream> PostByStreamAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            return new MemoryStream(await PostByBytesAsync(url, data, requestBodyFormat));
        }

        #endregion

        #endregion

        #region private methods

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求对象</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        private IRestResponse GetByPost(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = GetByPostAsync(url, data, requestBodyFormat);
            return res.Result;
        }

        /// <summary>
        /// Post请求 异步
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求文本对象</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        private async Task<IRestResponse> GetByPostAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var body = data;
            if (_requestBodyType.Id == RequestBodyType.TextXml.Id)
            {
                body = _xmlProvider.Serializer(data);
            }

            var request = GetProvider(RequestType.Post).GetRequest(Method.POST, url,
                new RequestBody(body, requestBodyFormat, _files),
                GetHeaders(), GetTimeOut());

            return await GetClient().ExecuteTaskAsync(request);
        }

        #endregion

        #endregion

        #region 设置Headers

        /// <summary>
        /// 设置headers
        /// </summary>
        /// <param name="name">请求头名称</param>
        /// <param name="value">请求头值</param>
        /// <param name="isOverload">是否覆盖 默认覆盖</param>
        public void AddHeaders(string name, string value, bool isOverload = true)
        {
            Headers = GetHeaders();
            if (Headers.Any(x => x.Key == name))
            {
                if (isOverload)
                {
                    Headers.Remove(name);
                }
                else
                {
                    return;
                }
            }

            Headers.Add(name, value);
        }

        /// <summary>
        /// 移除Headers
        /// </summary>
        /// <param name="name">请求头名称</param>
        public void RemoveHeaders(string name)
        {
            var headers = GetHeaders();
            if (headers.Any(x => x.Key == name))
            {
                headers.Remove(name);
            }
        }

        #endregion

        #region 添加文件

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="file">文件信息</param>
        public void AddFile(RequestMultDataParam file)
        {
            _files.Add(file);
        }

        #endregion

        #region 重置请求

        /// <summary>
        /// 重置请求
        /// 仅重置上传文件以及请求头信息、代理信息
        /// UserAgent以及编码格式不重置
        /// </summary>
        public void Reset()
        {
            _files = new List<RequestMultDataParam>();
            Headers = new Dictionary<string, string>();
            Proxy = null;
        }

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

        #region 得到请求Headers

        /// <summary>
        /// 得到请求Headers
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetHeaders()
        {
            return Headers ?? new Dictionary<string, string>();
        }

        #endregion

        #region 得到超时时间

        /// <summary>
        /// 得到超时时间
        /// </summary>
        /// <returns></returns>
        private int GetTimeOut()
        {
            return (TimeOut ?? 30000);
        }

        #endregion

        #region 得到RestClient

        /// <summary>
        /// 得到RestClient
        /// </summary>
        /// <returns></returns>
        private RestClient GetClient()
        {
            if (Proxy != null)
            {
                _restClient.Proxy = Proxy;
            }

            if (IsHttps)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) =>
                {
                    return true; //总是接受
                });
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            }

            _restClient.Encoding = _encoding;
            if (!string.IsNullOrEmpty(UserAgent))
            {
                _restClient.UserAgent = UserAgent;
            }

            return _restClient;
        }

        #endregion

        #endregion
    }
}
