// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain.Param;
using EInfrastructure.Core.CustomConfiguration.Core.Dto;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal.Data
{
    /// <summary>
    /// 应用名称空间
    /// </summary>
    public interface IAppNamespacesProvider : IPerRequest
    {
        /// <summary>
        /// 添加应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间</param>
        /// <param name="remark">备注</param>
        void Add(string appid, string name,  string remark);

        /// <summary>
        /// 更新应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间</param>
        /// <param name="param"></param>
        void Update(string appid, string name, EditAppNamespacesParam param);

        /// <summary>
        /// 删除应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        void Del(string appid, string name);

        /// <summary>
        /// 得到应用名称空间列表
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PageData<AppNamespacesItemDto> GetList(string appid, string name, int pageIndex, int pageSize);

        /// <summary>
        /// 得到应用名称空间详情
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        /// <returns></returns>
        AppNamespacesDetailDto Get(string appid, string name);
        
        /// <summary>
        /// 添加名称空间的值
        /// </summary>
        /// <param name="namespacesId">名称空间id</param>
        /// <param name="environmentName">环境信息</param>
        /// <param name="value">值</param>
        /// <param name="remark">备注</param>
        void AddItem(long namespacesId, string environmentName, string value, string remark);

        /// <summary>
        /// 得到名称空间下的值列表
        /// </summary>
        /// <param name="namespaceId">名称空间id</param>
        /// <param name="environmentName">环境信息</param>
        /// <returns></returns>
        List<NamespaceItemDto> GetItemList(string namespaceId,string environmentName);

        /// <summary>
        /// 根据值id获取值信息
        /// </summary>
        /// <param name="itemId">键id</param>
        /// <returns></returns>
        NamespaceDetailDto GetItem(long itemId);
    }
}