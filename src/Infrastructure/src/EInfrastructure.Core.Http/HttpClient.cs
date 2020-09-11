using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Logger;
using EInfrastructure.Core.Http.Common;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.Http.Params;
using EInfrastructure.Core.Http.Provider;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
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
            InternalHeaders = new Dictionary<string, string>();
            _requestBodyType = RequestBodyType.ApplicationJson;
            _requestBodyFormat = null;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="requestBodyFormat">等待响应的数据类型</param>
        /// <param name="requestBodyType">body请求类型</param>
        /// <param name="jsonProvider"></param>
        public HttpClient(string host,
            RequestBodyFormat requestBodyFormat = null,
            RequestBodyType requestBodyType = null, IJsonProvider jsonProvider = null) : this(host)
        {
            Host = host;
            _jsonProvider = jsonProvider ?? new NewtonsoftJsonProvider();
            _requestBodyType = requestBodyType ?? RequestBodyType.ApplicationJson;
            _requestBodyFormat = requestBodyFormat;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="requestBodyFormat">等待响应的数据类型</param>
        /// <param name="requestBodyType">请求类型</param>
        /// <param name="xmlProvider"></param>
        public HttpClient(string host,
            RequestBodyFormat requestBodyFormat = null,
            RequestBodyType requestBodyType = null, IXmlProvider xmlProvider = null) : this(host)
        {
            Host = host;
            _xmlProvider = xmlProvider ?? new XmlProvider();
            _requestBodyType = requestBodyType ?? RequestBodyType.ApplicationJson;
            _requestBodyFormat = requestBodyFormat;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="encoding">编码格式 默认Utf8</param>
        /// <param name="requestBodyType">请求类型</param>
        /// <param name="requestBodyFormat">等待响应的数据类型</param>
        /// <param name="jsonProvider"></param>
        public HttpClient(string host,
            RequestBodyType requestBodyType = null,
            RequestBodyFormat requestBodyFormat = null, IJsonProvider jsonProvider = null,
            Encoding encoding = null) : this(host,
            requestBodyFormat, requestBodyType,
            jsonProvider ?? new NewtonsoftJsonProvider())
        {
            Host = host;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// 请求接口域
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="requestBodyFormat">等待响应的数据类型</param>
        /// <param name="requestBodyType">请求类型</param>
        /// <param name="xmlProvider"></param>
        /// <param name="encoding">编码格式 默认Utf8</param>
        public HttpClient(string host,
            RequestBodyFormat requestBodyFormat = null,
            RequestBodyType requestBodyType = null, IXmlProvider xmlProvider = null, Encoding encoding = null) : this(
            host,
            requestBodyFormat,
            requestBodyType,
            xmlProvider ?? new XmlProvider())
        {
            Host = host;
            _encoding = encoding ?? Encoding.UTF8;
            _requestBodyFormat = requestBodyFormat;
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

            if (requestType.Id == RequestType.Post.Id)
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

            throw new BusinessException("不支持的请求", HttpStatus.Err.Id);
        }

        /// <summary>
        /// 日志
        /// </summary>
        private ILoggerProvider _logger;

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
        /// 得到响应的数据类型
        /// </summary>
        private RequestBodyFormat _requestBodyFormat;

        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string, string> Headers;

        /// <summary>
        /// 请求头
        /// </summary>
        internal Dictionary<string, string> InternalHeaders;

        /// <summary>
        /// Cookie信息
        /// </summary>
        public List<KeyValuePair<string, string>> Cookies;

        /// <summary>
        /// Cookie信息
        /// </summary>
        internal List<KeyValuePair<string, string>> InternalCookies;

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

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public string GetString(string url)
        {
            var request = GetRequest(url);
            var content = GetRestResponse(GetClient(), request).Content;
            AddLog(request, content);
            return content;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public string GetString(string url, object data)
        {
            return GetString(SetUrlParam(url, GetParams(data)));
        }

        #endregion

        #region 得到响应对象

        #region 响应信息为Json对象

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public T GetJson<T>(string url)
            where T : class, new()
        {
            var res = GetString(url);
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
            where T : class, new()
        {
            var res = GetString(url, GetParams(data));
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
            where T : class, new()
        {
            var res = GetString(url);
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
            where T : class, new()
        {
            var res = GetString(url, GetParams(data));
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
        }

        #endregion

        #endregion

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public byte[] GetBytes(string url)
        {
            var request = GetRequest(url);
            return GetRestResponse(GetClient(), request).RawBytes;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public byte[] GetBytes(string url, object data)
        {
            return GetBytes(SetUrlParam(url, GetParams(data)));
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

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url)
        {
            var request = GetRequest(url);
            var content = (await GetRestResponseAsync(GetClient(), request)).Content;
            AddLog(request, content);
            return content;
        }

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url, object data)
        {
            return await GetStringAsync(SetUrlParam(url, GetParams(data)));
        }

        #endregion

        #region 得到响应对象

        #region 响应信息为Json对象

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public async Task<T> GetJsonAsync<T>(string url)
        {
            var res = await GetStringAsync(url);
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
        public async Task<T> GetJsonAsync<T>(string url, object data)
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
        public async Task<T> GetXmlAsync<T>(string url, object data)
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

        #region get请求得到byte数组

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url)
        {
            var request = GetRequest(url);
            return (await GetRestResponseAsync(GetClient(), request)).RawBytes;
        }

        /// <summary>
        /// get请求得到byte数组
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync(string url, object data)
        {
            return await GetBytesAsync(SetUrlParam(url, GetParams(data)));
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
        private RestRequest GetRequest(string url)
        {
            return GetProvider(RequestType.Get)
                .GetRequest(Method.GET, url, new RequestBody(null), GetHeaders(), GetTimeOut());
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
            where T : class, new()
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
            where T : class, new()
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
            var request = GetByPost(url, data, requestBodyFormat);
            var content = GetRestResponse(GetClient(), request).Content;
            AddLog(request, content);
            return content;
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
            var request = GetByPost(url, data, requestBodyFormat);
            return GetRestResponse(GetClient(), request).RawBytes;
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

        #region Post请求得到响应内容

        /// <summary>
        /// Post请求得到响应内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <returns></returns>
        public async Task<string> GetPostByStringAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await GetByPostAsync(url, data, requestBodyFormat);
            return res.Content;
        }

        #endregion

        #region Post请求得到响应信息为Json对象

        /// <summary>
        /// Post请求得到响应信息为Json对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="requestBodyFormat">请求类型格式化 默认为Json</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetJsonByPostAsync<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await GetPostByStringAsync(url, data, requestBodyFormat);
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
        public async Task<T> GetXmlByPostAsync<T>(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var res = await GetPostByStringAsync(url, data, requestBodyFormat);
            if (string.IsNullOrEmpty(res))
            {
                return default(T);
            }

            return _xmlProvider.Deserialize<T>(res, _encoding);
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
        public async Task<byte[]> GetBytesByPostAsync(string url, object data,
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
        public async Task<Stream> GetStreamByPostAsync(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            return new MemoryStream(await GetBytesByPostAsync(url, data, requestBodyFormat));
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
        private RestRequest GetByPost(string url, object data,
            RequestBodyFormat requestBodyFormat = null)
        {
            var body = _requestBodyType.Id == RequestBodyType.TextXml.Id
                ? _xmlProvider.Serializer(data)
                : data;
            var request = GetProvider(RequestType.Post).GetRequest(Method.POST, url,
                new RequestBody(body, GetRequestBody(requestBodyFormat), _files, _jsonProvider, _xmlProvider),
                GetHeaders(), GetTimeOut());
            return request;
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
            var body = _requestBodyType.Id == RequestBodyType.TextXml.Id
                ? _xmlProvider.Serializer(data)
                : _jsonProvider.Serializer((data ?? new { }));
            var request = GetProvider(RequestType.Post).GetRequest(Method.POST, url,
                new RequestBody(body, GetRequestBody(requestBodyFormat), _files, _jsonProvider, _xmlProvider),
                GetHeaders(), GetTimeOut());
            return await GetRestResponseAsync(GetClient(), request);
        }

        #endregion

        #endregion

        #region Headers管理

        #region 设置headers

        /// <summary>
        /// 设置headers
        /// </summary>
        /// <param name="key">请求头名称</param>
        /// <param name="value">请求头值</param>
        /// <param name="isOverload">是否覆盖 默认覆盖</param>
        public void AddHeader(string key, string value, bool isOverload = true)
        {
            InternalHeaders = GetHeaders();
            if (InternalHeaders.Any(x => x.Key == key))
            {
                if (isOverload)
                {
                    InternalHeaders.Remove(key);
                }
                else
                {
                    return;
                }
            }

            InternalHeaders.Add(key, value);
        }

        #endregion

        #region 移除指定的Header

        /// <summary>
        /// 移除指定的Header
        /// </summary>
        /// <param name="name">请求头名称</param>
        public void RemoveHeader(string name)
        {
            var headers = GetHeaders();
            if (headers.Any(x => x.Key == name))
            {
                headers.Remove(name);
            }
        }

        #endregion

        #region 清空Header

        /// <summary>
        /// 清空Header
        /// </summary>
        public void ClearHeaders()
        {
            InternalHeaders = new Dictionary<string, string>();
            InternalCookies=new List<KeyValuePair<string, string>>();
        }

        #endregion

        #endregion

        #region Cookie管理

        #region 添加Cookie

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isOverload">是否覆盖 默认覆盖</param>
        public void AddCookie(string key, string value, bool isOverload = true)
        {
            InternalCookies = GetCookies();
            if (InternalCookies.Any(x => x.Key == key))
            {
                if (isOverload)
                {
                    InternalHeaders.Remove(key);
                }
                else
                {
                    return;
                }
            }

            InternalHeaders.Add(key, value);
        }

        #endregion

        #region 移除Cookie

        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="key">键</param>
        private void RemoveCookie(string key)
        {
            var cookies = GetCookies();
            if (cookies.Any(x => x.Key == key))
            {
                cookies.Remove(cookies.FirstOrDefault(x => x.Key == key));
            }
        }

        #endregion

        #region 清空Cookie

        /// <summary>
        /// 清空Cookie
        /// </summary>
        public void ClearCookie()
        {
            InternalCookies = new List<KeyValuePair<string, string>>();
            InternalHeaders = GetHeaders().Where(x => !x.Key.Equals("Cookie", StringComparison.CurrentCultureIgnoreCase))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

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
            InternalHeaders = new Dictionary<string, string>();
            Proxy = null;
            _requestBodyFormat = null;
        }

        #endregion

        #region 设置请求日志

        /// <summary>
        /// 设置请求日志
        /// </summary>
        /// <param name="logger"></param>
        public void UseLogger(ILoggerProvider logger)
        {
            this._logger = logger;
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
            if (data is Dictionary<string, string> dictionary)
            {
                return dictionary;
            }

            if (data is Dictionary<object, object>)
            {
                throw new BusinessException("暂不支持字典类型的Data，除非Data是Dictionary<string,string>", HttpStatus.Err.Id);
            }

            return ObjectCommon.GetParams(data,
                    "Microsoft.AspNetCore.Mvc.FromQueryAttribute,Microsoft.AspNetCore.Mvc.Core",
                    res => _jsonProvider.Serializer(data))
                .ToDictionary(x => x.Key, x => x.Value.ToString());
        }

        #endregion

        #region 得到请求Headers

        /// <summary>
        /// 得到请求Headers
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetHeaders()
        {
            var headers = InternalHeaders ?? new Dictionary<string, string>();
            return headers;
        }

        #endregion

        #region 得到请求Cookie

        /// <summary>
        /// 得到请求Cookie
        /// </summary>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> GetCookies()
        {
            var cookies = InternalCookies ?? new List<KeyValuePair<string, string>>();
            return cookies;
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
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
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

        #region 得到RequestBodyFormat

        /// <summary>
        /// 得到RequestBodyFormat
        /// </summary>
        /// <param name="requestBodyFormat">数据响应类型</param>
        /// <returns></returns>
        private RequestBodyFormat GetRequestBody(RequestBodyFormat requestBodyFormat)
        {
            return requestBodyFormat ?? _requestBodyFormat;
        }

        #endregion

        #region 记录日志

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        private void AddLog(RestRequest request, string content)
        {
            if (_logger != null)
            {
                string header = "";
                var list = request.Parameters.Where(x => x.Type == ParameterType.HttpHeader)
                    .Select(x => $"key：{x.Name}，value：{x.Value}").ToList();
                list.ForEach(item => { header += $"{item}，"; });
                if (!string.IsNullOrEmpty(header))
                {
                    header = header.Substring(0, header.Length - 1);
                }

                _logger.LogDebug(
                    $"url：{request.Resource}，method:{request.Method.ToString()}，timeOut：{request.Timeout}，Header：{header}，result：{content}");
            }
        }

        #endregion

        #region 得到响应信息

        /// <summary>
        /// 得到响应信息
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private IRestResponse GetRestResponse(RestClient restClient, RestRequest request)
        {
            return restClient.Execute(request);
        }

        /// <summary>
        /// 得到响应信息
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<IRestResponse> GetRestResponseAsync(RestClient restClient, RestRequest request)
        {
            return restClient.ExecuteTaskAsync(request);
        }

        #endregion

        #endregion
    }
}
