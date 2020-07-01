// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Params
{
    /// <summary>
    /// 图片鉴别
    /// </summary>
    public class PictureCensorParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="persistentOps">基础策略</param>
        /// <param name="types">鉴别分类集合</param>
        public PictureCensorParam(BasePersistentOps persistentOps = null, params ContentType[] types)
        {
            Types = types?.ToList() ?? new List<ContentType>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="paramList">如果IsUrl为true，则为图片Url集合，否则为图片base64集合</param>
        /// <param name="isUrl">是否图片Url(默认是图片url，若为false，则为图片base64)</param>
        /// <param name="persistentOps">基础策略</param>
        /// <param name="types">鉴别分类集合</param>
        public PictureCensorParam(List<string> paramList, bool isUrl, BasePersistentOps persistentOps = null,
            params ContentType[] types) : this(persistentOps, types)
        {
            if (isUrl)
            {
                Urls = paramList;
            }
            else
            {
                Base64S = paramList;
            }
        }

        /// <summary>
        /// 图片url地址集合
        /// </summary>
        public List<string> Urls { get; private set; }

        /// <summary>
        /// 图片base64集合
        /// </summary>
        public List<string> Base64S { get; private set; }

        /// <summary>
        /// 鉴别分类
        /// </summary>
        public List<ContentType> Types { get; private set; }

        /// <summary>
        /// 基础策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }

        #region 业务扩展参数

        /// <summary>
        /// 用户IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 用户唯一标识，如果无需登录则为空
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户设备类型，1：web， 2：wap， 3：android， 4：iphone， 5：ipad， 6：pc， 7：wp
        /// </summary>
        public DeviceSubType DeviceType { get; set; }

        /// <summary>
        /// 用户设备 id
        /// </summary>
        public string DeviceId { get; set; }

        #endregion

        #region 反垃圾版防盗刷（需联系专属客服）

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 来自易盾反作弊SDK返回的token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extension { get; set; }

        #endregion

        #region methods

        #region 添加Url

        /// <summary>
        /// 添加Url
        /// </summary>
        /// <param name="urls"></param>
        public void AddUrl(params string[] urls)
        {
            (urls?.ToList() ?? new List<string>()).ForEach(url =>
            {
                if (!string.IsNullOrEmpty(url))
                {
                    this.Urls.Add(url);
                }
            });
        }

        #endregion

        #region 添加图片Base64

        /// <summary>
        /// 添加图片Base64
        /// </summary>
        /// <param name="base64S"></param>
        public void AddBase64(params string[] base64S)
        {
            (base64S?.ToList() ?? new List<string>()).ForEach(base64 =>
            {
                if (!string.IsNullOrEmpty(base64))
                {
                    this.Base64S.Add(base64);
                }
            });
        }

        #endregion

        #endregion
    }
}
