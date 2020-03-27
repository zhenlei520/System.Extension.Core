// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using EInfrastructure.Core.AliYun.DaYu.Config;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 短信帮助类
    /// </summary>
    public abstract class BaseSmsProvider
    {
        /// <summary>
        ///
        /// </summary>
        protected AliSmsConfig _smsConfig;

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsConfig"></param>
        public BaseSmsProvider(AliSmsConfig smsConfig)
        {
            _smsConfig = smsConfig;
        }

        #region 得到Client

        /// <summary>
        /// 得到Client
        /// </summary>
        /// <returns></returns>
        protected virtual IAcsClient GetClient()
        {
            DefaultProfile profile =
                DefaultProfile.GetProfile("cn-hangzhou", _smsConfig.AccessKey, _smsConfig.EncryptionKey);
            return new DefaultAcsClient(profile);
        }

        #endregion

        #region 得到公共的请求参数

        /// <summary>
        /// 得到公共的请求参数
        /// </summary>
        /// <returns></returns>
        protected virtual CommonRequest GetRequest()
        {
            return new CommonRequest
            {
                Method = MethodType.POST,
                Domain = "dysmsapi.aliyuncs.com",
                Version = "2017-05-25",
                Action = "SendSms",
                RegionId = "cn-hangzhou",
            };
        }

        #endregion
    }
}
