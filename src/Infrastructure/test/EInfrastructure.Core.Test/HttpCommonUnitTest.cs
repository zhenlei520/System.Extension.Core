// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Net;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Http.Params;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
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

        [Fact]
        public string GetString()
        {
            HttpClient client = new HttpClient("https://api.weixin.qq.com");
            string client_credential = "";
            string appid = "";
            string secret = "";
            var str = client.GetString($"cgi-bin/token?grant_type={client_credential}&appid={appid}&secret={secret}");
            return str;
        }


        [Fact]
        public WeChatResponseDto GetJson()
        {
            HttpClient client = new HttpClient("https://api.weixin.qq.com");
            string client_credential = "";
            string appid = "";
            string secret = "";
            var res = client.GetJson<WeChatResponseDto>(
                $"cgi-bin/token?grant_type={client_credential}&appid={appid}&secret={secret}");
            return res;
        }

        public class WeChatResponseDto
        {
            /// <summary>
            /// AccessToken
            /// </summary>
            [JsonProperty(PropertyName = "access_token",DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string AccessToken { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            [JsonProperty(PropertyName = "expires_in", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public int? expires_in { get; set; }

            /// <summary>
            /// 错误码
            /// </summary>
            [JsonProperty(PropertyName = "errcode",DefaultValueHandling = DefaultValueHandling.Ignore)]
            public int? Errcode { get; set; }

            /// <summary>
            /// 错误原因
            /// </summary>
            [JsonProperty(PropertyName = "errmsg",DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string Errmsg { get; set; }
        }

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
