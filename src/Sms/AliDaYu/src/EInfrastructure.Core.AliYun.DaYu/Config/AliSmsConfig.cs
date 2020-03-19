// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Validation;

namespace EInfrastructure.Core.AliYun.DaYu.Config
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class AliSmsConfig : IFluentlValidatorEntity
    {
        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// AccessKey ID
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 秘钥参数
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
