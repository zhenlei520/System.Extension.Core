// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;

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



        #region private methods

        /// <summary>
        /// 短信验证码
        /// </summary>
        protected readonly Dictionary<string, SmsCode> SmsCodeMap = new Dictionary<string, SmsCode>()
        {
            {"OK", SmsCode.Ok},
            {"isv.TEMPLATE_MISSING_PARAMETERS", SmsCode.TemplateIllegal},
            {"isv.SMS_TEMPLATE_ILLEGAL", SmsCode.TemplateIllegal},
            {"isv.SMS_SIGNATURE_ILLEGAL", SmsCode.SignIllegal},
            {"isv.MOBILE_NUMBER_ILLEGAL", SmsCode.MobileNumberIllegal},
            {"isv.BUSINESS_LIMIT_CONTROL", SmsCode.BusinessLimitControl},
            {"isv.AMOUNT_NOT_ENOUGH", SmsCode.AmountNotEnough},
            {"isp.RAM_PERMISSION_DENY", SmsCode.InsufficientPrivileges},
            {"isv.OUT_OF_SERVICE", SmsCode.BusinessStop},
            {"isv.ACCOUNT_NOT_EXISTS", SmsCode.AbnormalAccount},
            {"isv.ACCOUNT_ABNORMAL", SmsCode.AbnormalAccount},
            {"isv.BLACK_KEY_CONTROL_LIMIT", SmsCode.BlackKeyControlLimit},
            {"isv.INVALID_PARAMETERS", SmsCode.InvalidParameters},
            {"isv.PARAM_LENGTH_LIMIT", SmsCode.LengthError},
            {"isv.INVALID_JSON_PARAM", SmsCode.InvalidParameters},
            {"MissingAccessKeyId", SmsCode.AccessKeyError},
        };

        #endregion
    }
}
