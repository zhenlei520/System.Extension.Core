// Copyright (c) zhenlei520 All rights reserved.

using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public partial class Extensions
    {
        #region Sha加密

        #region 得到sha系列加密信息

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="retval"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha(this byte[] retval, HashAlgorithm hashAlgorithm, bool isUpper)
        {
            var data = hashAlgorithm.ComputeHash(retval);
            StringBuilder sc = new StringBuilder();
            foreach (var t in data)
            {
                sc.Append(isUpper ? t.ToString("X2") : t.ToString("x2"));
            }

            return sc.ToString();
        }

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="hashAlgorithm">加密方式</param>
        /// <param name="isUpper">是否大写</param>
        /// <returns></returns>
        public static string GetSha(this Stream stream, HashAlgorithm hashAlgorithm, bool isUpper = true)
        {
            if (stream == null)
            {
                throw new BusinessException("FileStream is Null", HttpStatus.Err.Id);
            }

            byte[] retval = hashAlgorithm.ComputeHash(stream);
            stream.Close();
            return GetSha(retval, hashAlgorithm, isUpper);
        }

        #endregion

        #endregion
    }
}
