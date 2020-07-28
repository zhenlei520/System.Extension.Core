// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain.Param;
using EInfrastructure.Core.CustomConfiguration.Core.Dto;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal.Data
{
    /// <summary>
    /// App
    /// </summary>
    public interface IAppProvider: IPerRequest
    {
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        void Add(string appid, string name);

        /// <summary>
        /// 更新应用
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="param">新应用信息</param>
        void Update(string appid, EditAppParam param);

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appid">应用id</param>
        void Del(string appid);

        /// <summary>
        /// 得到应用集合
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PageData<AppItemDto> GetList(string keyword, int pageIndex, int pageSize);

        /// <summary>
        /// 得到app信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        AppDetailDto Get(string appid);
    }
}