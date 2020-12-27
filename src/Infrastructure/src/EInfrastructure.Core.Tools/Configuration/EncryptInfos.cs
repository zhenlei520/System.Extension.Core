// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text;

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// 基础信息
    /// </summary>
    public class EncryptBaseInfos
    {
        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encoding { get; set; }
    }

    /// <summary>
    /// 加密信息
    /// </summary>
    public class EncryptInfos : EncryptBaseInfos
    {
        /// <summary>
        ///
        /// </summary>
        public EncryptInfos() : this(Encoding.UTF8)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="encoding">编码方式</param>
        public EncryptInfos(Encoding encoding)
        {
            base.Encoding = encoding;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <param name="encoding">编码方式</param>
        public EncryptInfos(string key, string iv, Encoding encoding) : this(encoding)
        {
            Key = key;
            Iv = iv;
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 向量
        /// </summary>
        public string Iv { get; set; }
    }
}
