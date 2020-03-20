// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Net;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Http.Params;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class HttpCommonUnitTest : BaseUnitTest
    {
        public HttpCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        #region Get请求

        /// <summary>
        /// 得到client
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient()
        {
            return new HttpClient("https://api.weixin.qq.com");
        }

        /// <summary>
        /// 得到AccessToken的请求参数
        /// </summary>
        public class AccessTokenParam
        {
            /// <summary>
            ///
            /// </summary>
            [FromQuery(Name = "appid")]
            public string Appid { get; set; }

            /// <summary>
            /// 秘钥
            /// </summary>
            [FromQuery(Name = "secret")]
            public string Secret { get; set; }

            /// <summary>
            ///
            /// </summary>
            [FromQuery(Name = "grant_type")]
            public string GrantType { get; set; }
        }

        #region Get请求 得到响应字符串

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <returns></returns>
        [Fact]
        public string GetString1()
        {
            HttpClient client = GetClient();
            string client_credential = "";
            string appid = "";
            string secret = "";
            var str = client.GetString($"cgi-bin/token?grant_type={client_credential}&appid={appid}&secret={secret}");
            return str;
        }

        #endregion

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <returns></returns>
        [Fact]
        public string GetString2()
        {
            HttpClient client = GetClient();
            string client_credential = "";
            string appid = "";
            string secret = "";
            var str = client.GetString($"cgi-bin/token",
                new
                {
                    grant_type = client_credential,
                    appid = appid,
                    secret = secret
                });
            return str;
        }

        #endregion

        #region Get请求 得到响应字符串

        /// <summary>
        /// Get请求 得到响应字符串
        /// </summary>
        /// <returns></returns>
        [Fact]
        public string GetString3()
        {
            HttpClient client = GetClient();
            string client_credential = "";
            string appid = "";
            string secret = "";
            var str = client.GetString($"cgi-bin/token",
                new AccessTokenParam
                {
                    GrantType = client_credential,
                    Appid = appid,
                    Secret = secret
                });
            return str;
        }

        #endregion

        #endregion

        #region Get请求 得到响应Json对象

        #region Get请求 得到响应Json对象

        /// <summary>
        /// Get请求 得到响应Json对象
        /// </summary>
        /// <returns></returns>
        [Fact]
        public WeChatResponseDto GetJson1()
        {
            HttpClient client = new HttpClient("https://api.weixin.qq.com");
            string client_credential = "";
            string appid = "";
            string secret = "";
            var res = client.GetJson<WeChatResponseDto>(
                $"cgi-bin/token?grant_type={client_credential}&appid={appid}&secret={secret}");
            return res;
        }

        #endregion

        #region Get请求 得到响应Json对象

        /// <summary>
        /// Get请求 得到响应Json对象
        /// </summary>
        /// <returns></returns>
        [Fact]
        public WeChatResponseDto GetJson2()
        {
            HttpClient client = new HttpClient("https://api.weixin.qq.com");
            string client_credential = "";
            string appid = "";
            string secret = "";
            var res = client.GetJson<WeChatResponseDto>(
                $"cgi-bin/token", new
                {
                    grant_type = client_credential,
                    appid = appid,
                    secret = secret
                });
            return res;
        }

        #endregion

        #region Get请求 得到响应Json对象

        /// <summary>
        /// Get请求 得到响应Json对象
        /// </summary>
        /// <returns></returns>
        [Fact]
        public WeChatResponseDto GetJson3()
        {
            HttpClient client = new HttpClient("https://api.weixin.qq.com");
            string client_credential = "";
            string appid = "";
            string secret = "";
            var res = client.GetJson<WeChatResponseDto>(
                $"cgi-bin/token", new AccessTokenParam
                {
                    GrantType = client_credential,
                    Appid = appid,
                    Secret = secret
                });
            return res;
        }

        #endregion

        /// <summary>
        /// 微信响应信息
        /// </summary>
        public class WeChatResponseDto
        {
            /// <summary>
            /// AccessToken
            /// </summary>
            [JsonProperty(PropertyName = "access_token", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string AccessToken { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            [JsonProperty(PropertyName = "expires_in", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public int? expires_in { get; set; }

            /// <summary>
            /// 错误码
            /// </summary>
            [JsonProperty(PropertyName = "errcode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public int? Errcode { get; set; }

            /// <summary>
            /// 错误原因
            /// </summary>
            [JsonProperty(PropertyName = "errmsg", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string Errmsg { get; set; }
        }

        #endregion

        #endregion

        #region Post

        [Fact]
        public string GetStringByPost()
        {
            HttpClient client = new HttpClient("https://ai.baidu.com")
            {
                Headers = new Dictionary<string, string>()
                {
                    {"Cookie", ""}
                }
            };
            var file = new FileInfo("D:/jetbrains.png");
            var array = file.OpenRead().ConvertToByteArray();
            client.AddFile(new RequestMultDataParam("image", file.Name, array, "image/png"));
            var res = client.GetStringByPost($"aidemo", new
            {
                image_url = "",
                type = "animal",
                show = true
            });
            return res;
        }

        [Fact]
        public ResponseDto GetJsonByPost()
        {
            HttpClient client = new HttpClient("https://ai.baidu.com")
            {
                Headers = new Dictionary<string, string>()
                {
                    {"Cookie", ""}
                }
            };
            var file = new FileInfo("D:/jetbrains.png");
            var array = file.OpenRead().ConvertToByteArray();
            client.AddFile(new RequestMultDataParam("image", file.Name, array, "image/png"));
            var res = client.GetJsonByPost<ResponseDto>($"aidemo", new
            {
                image_url = "",
                type = "animal",
                show = true
            });
            return res;
        }

        public class ResponseDto
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty(PropertyName = "errno")]
            public int Errno { get; set; }

            /// <summary>
            /// Msg
            /// </summary>
            [JsonProperty(PropertyName = "msg")]
            public string Msg { get; set; }

            /// <summary>
            /// Data
            /// </summary>
            [JsonProperty(PropertyName = "data")]
            public object Data { get; set; }
        }

        #endregion
    }
}
