// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.Internal;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 短链接/短参数帮助类
    /// </summary>
    public class ShortenCommon
    {
        private static IShortenProvider _shortenProviders;

        /// <summary>
        ///
        /// </summary>
        public ShortenCommon()
        {
            _shortenProviders=new ServiceProvider().GetServices<IShortenProvider>().OrderByDescending(x=>x.GetWeights()).FirstOrDefault()??throw new NotImplementedException(nameof(_shortenProviders));
        }

        private const string ShortKey = "EInfrastructure.Core";

        #region 得到短参数信息

        /// <summary>
        /// 得到短参数信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="number">生成短连接的长度</param>
        /// <returns>得到短参数的值（有四个，任选其一即可）</returns>
        public static string[] GetShortParam(string param, int number = 6)
        {
            return GetShortParam(param, ShortKey, number);
        }

        /// <summary>
        /// 得到短参数信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="shortKey">md5盐（默认）</param>
        /// <param name="number">生成短连接的长度</param>
        /// <returns>得到短参数的值（有四个，任选其一即可）</returns>
        public static string[] GetShortParam(string param, string shortKey,int number = 6)
        {
            return _shortenProviders.GetShortParam(param, shortKey, number);
        }

        #endregion
    }
}
