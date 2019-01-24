using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using Newtonsoft.Json;
using RestSharp;

namespace EInfrastructure.Core.AliYun.Tbk.Common
{
    public class TaoBaoCommon
    {
        /// <summary>
        /// 普通url地址列表
        /// </summary>
        private readonly List<string> _normalUrlList = new List<string>()
        {
            ".tmall.",
            ".taobao.com",
            ".liangxinyao.com",
            ".yao.95095.com"
        };

        private int _againNaughtyCount = 3; //重试获取商品url地址次数

        private string _originUrl; //商品来源地址

        #region 获取并检查商品id
        /// <summary>
        /// 获取并检查商品id
        /// </summary>
        /// <param name="url">商品url地址或者短连接地址</param>
        /// <returns></returns>
        public string GetGoodIdAndCheck(string url)
        {
            if (IsShortUrl())
            {
                //未解密淘口令短连接
                url = GetGoodUrl(url);
            }
            return url;

            //判断是否是短连接
            bool IsShortUrl()
            {
                foreach (var item in _normalUrlList)
                {
                    if (url.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

        #region 获取商品url地址

        /// <summary>
        /// 获取商品url地址
        /// </summary>
        /// <param name="mUrl">口令中的url地址</param>
        /// <returns></returns>
        private string GetGoodUrl(string mUrl)
        {
            Uri uri = new Uri(mUrl);
            string host = uri.Host;
            int tryAgainCount = 0; //当前重试次数
            again:
            RestClient restClient = new RestClient("http://"+ uri.Host);
            restClient.UserAgent = "User-Agent:Mozilla/5.0(iPhone;U;CPUiPhoneOS4_3_3likeMacOSX;en-us)AppleWebKit/533.17.9(KHTML,likeGecko)Version/5.0.2Mobile/8J2Safari/6533.18.5";

            RestRequest request = new RestRequest(uri.PathAndQuery, Method.GET);

            string response = restClient.Execute(request).Content;
            GetOriginUrl();
            for (int i = tryAgainCount; i < _againNaughtyCount; i++)
            {
                if (!string.IsNullOrEmpty(_originUrl))
                {
                    return _originUrl;
                }
                tryAgainCount++;
                goto again;
            }

            void GetOriginUrl()
            {
                string regexStr = "var url = '(.*)';";
                if (Regex.Match(response, regexStr).Success)
                {
                    //淘宝，例如：http://www.dwntme.com/h.ZZllOcZ
                    _originUrl = Regex.Match(response, regexStr).Groups[1].Value.UrlDecode();
                }
                regexStr = "var pageData = (.*);";
                if (Regex.Match(response, regexStr).Success)
                {
                    //天猫，例如：http://zmnxbc.com/s/f8E1c?tm=4dbc9e
                    var postData = Regex.Match(response, regexStr).Groups[1].Value.UrlDecode();
                    if (!string.IsNullOrEmpty(postData))
                    {
                        var dataInfo = JsonConvert.DeserializeObject<dynamic>(postData);
                        if (dataInfo != null)
                        {
                            string goodsId = dataInfo.bizId; //商品id
                            double goodsIdtemp;
                            if (double.TryParse(goodsId, out goodsIdtemp))
                            {
                                _originUrl = $"https://detail.tmall.com/item.htm?id={dataInfo.bizId}"; //伪造跳转url
                            }
                            else
                            {
                                _originUrl = dataInfo.actionRule[0].url;
                                if (!_originUrl.Contains("https:"))
                                {
                                    _originUrl = $"https:{_originUrl}";
                                }
                            }
                        }
                    }
                }
            }
            throw new BusinessException("获取商品真实地址出错");
        }

        #endregion

        /// <summary>
        /// 判断是否是淘口令
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsAmoyPsd(string str, ref string code)
        {
            Regex reg = new Regex("￥.*￥", RegexOptions.Multiline);
            MatchCollection matchs = reg.Matches(str);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    code = item.Value;
                    return true;
                }
            }
            return false;
        }
    }
}