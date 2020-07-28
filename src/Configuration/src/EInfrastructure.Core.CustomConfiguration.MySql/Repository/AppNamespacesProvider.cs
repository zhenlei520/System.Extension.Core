// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Config.Entities.Extensions;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.CustomConfiguration.Core.Domain.Param;
using EInfrastructure.Core.CustomConfiguration.Core.Dto;
using EInfrastructure.Core.CustomConfiguration.Core.Internal.Data;
using EInfrastructure.Core.MySql.Repository;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.CustomConfiguration.MySql.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class AppNamespacesProvider : IAppNamespacesProvider
    {
        private readonly IAppNamespacesRepository _repository;
        private readonly IQuery<AppNamespaces, long, CustomerConfigurationDbContext> _query;
        private readonly IQuery<Apps, long, CustomerConfigurationDbContext> _appQuery;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="query"></param>
        /// <param name="appQuery"></param>
        internal AppNamespacesProvider(IAppNamespacesRepository repository,
            IQuery<AppNamespaces, long, CustomerConfigurationDbContext> query,
            IQuery<Apps, long, CustomerConfigurationDbContext> appQuery)
        {
            this._repository = repository;
            this._query = query;
            this._appQuery = appQuery;
        }

        #region 添加应用名称空间

        /// <summary>
        /// 添加应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间</param>
        /// <param name="remark">备注</param>
        public void Add(string appid, string name, string remark)
        {
            CheckApp(appid);
            CheckNamespaceName(name);
            CheckNamespaceRemark(remark);

            AppNamespaces appNamespaces = this._repository.Get(appid, name);
            if (appNamespaces != null)
            {
                throw new BusinessException($"{name}在应用{appid}中已存在");
            }

            appNamespaces = new AppNamespaces(appid, name, "json", remark);
            this._repository.Add(appNamespaces);
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 更新应用名称空间

        /// <summary>
        /// 更新应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间</param>
        /// <param name="param"></param>
        public void Update(string appid, string name, EditAppNamespacesParam param)
        {
            new EditAppNamespacesParam.EditAppNamespacesParamValidator().Validate(param).Check(HttpStatus.Err.Name);

            CheckApp(appid);

            var appNamespaces = this._repository.Get(appid, name);
            if (appNamespaces == null)
            {
                throw new BusinessException("当前名称空间不存在或者已经被删除");
            }

            if (this._query.GetQueryable().Any(x =>
                x.Id != appNamespaces.Id && !x.IsDel && x.AppId == appid && x.Name == param.Name))
            {
                throw new BusinessException("当前appid已存在，修改失败");
            }

            appNamespaces.Update(param.Name, param.Remark);
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 删除应用名称空间

        /// <summary>
        /// 删除应用名称空间
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        public void Del(string appid, string name)
        {
            var appNamespaces = this._repository.Get(appid, name);
            appNamespaces?.Del();
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 得到应用名称空间列表

        /// <summary>
        /// 得到应用名称空间列表
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">名称空间名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PageData<AppNamespacesItemDto> GetList(string appid, string name, int pageIndex, int pageSize)
        {
            CheckApp(appid);
            return _query.GetQueryable().Where(x => x.AppId == appid && x.Name == name && !x.IsDel).Select(x =>
                new AppNamespacesItemDto()
                {
                    Id = x.Id,
                    AppId = x.AppId,
                    Name = x.Name,
                    Format = x.Format,
                    Remark = x.Remark,
                    EditTime = x.EditTime
                }).ListPager(pageSize, pageIndex, true);
        }

        #endregion

        #region 得到应用名称空间详情

        /// <summary>
        /// 得到应用名称空间详情
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        /// <returns></returns>
        public AppNamespacesDetailDto Get(string appid, string name)
        {
            CheckApp(appid);
            return this._query.GetQueryable().Where(x => x.AppId == appid && x.Name == name && !x.IsDel).Select(x =>
                new AppNamespacesDetailDto()
                {
                    Id = x.Id,
                    AppId = x.AppId,
                    Name = x.Name,
                    Format = x.Format,
                    Remark = x.Remark,
                    AddTime = x.AddTime,
                    EditTime = x.EditTime
                }).FirstOrDefault();
        }

        #endregion

        #region 添加名称空间的值

        /// <summary>
        /// 添加名称空间的值
        /// </summary>
        /// <param name="namespacesId">名称空间id</param>
        /// <param name="environmentName">环境信息</param>
        /// <param name="value">值</param>
        /// <param name="remark">备注</param>
        public void AddItem(long namespacesId, string environmentName, string value, string remark)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 得到名称空间下的值列表

        /// <summary>
        /// 得到名称空间下的值列表
        /// </summary>
        /// <param name="namespaceId">名称空间id</param>
        /// <param name="environmentName">环境信息</param>
        /// <returns></returns>
        public List<NamespaceItemDto> GetItemList(string namespaceId, string environmentName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 根据值id获取值信息

        /// <summary>
        /// 根据值id获取值信息
        /// </summary>
        /// <param name="itemId">键id</param>
        /// <returns></returns>
        public NamespaceDetailDto GetItem(long itemId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region private methods

        #region 检查app信息

        /// <summary>
        /// 检查app信息
        /// </summary>
        /// <param name="appid">应用id</param>
        void CheckApp(string appid)
        {
            if (!_appQuery.Exists(x => x.AppId == appid && !x.IsDel))
            {
                throw new BusinessException("应用不存在或者已经被删除");
            }
        }

        #endregion

        #region 检查名称空间名

        /// <summary>
        /// 检查名称空间名
        /// </summary>
        /// <param name="name">名称空间名</param>
        void CheckNamespaceName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BusinessException("name is not empty");
            }

            if (name.Length > 50)
            {
                throw new BusinessException("the name length is less than or equal to 50");
            }

            string format = PathCommon.GetExtension(name).ToLower();
            if (!format.Equals(".json"))
            {
                throw new BusinessException("暂时不支持json之外的格式");
            }
        }

        #endregion

        #region 检查名称空间备注

        /// <summary>
        /// 检查名称空间备注
        /// </summary>
        /// <param name="remark">名称空间备注</param>
        void CheckNamespaceRemark(string remark)
        {
            if (remark != null && remark.Length > 200)
            {
                throw new BusinessException("the remark length is less than or equal to 200");
            }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 应用仓储
    /// </summary>
    public interface IAppNamespacesRepository : IRepository<AppNamespaces, long, CustomerConfigurationDbContext>,
        IPerRequest
    {
        /// <summary>
        /// 根据appid得到应用信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称空间</param>
        /// <returns></returns>
        AppNamespaces Get(string appid, string name);
    }

    /// <summary>
    /// 应用仓储
    /// </summary>
    public class AppNamespacesRepository : RepositoryBase<AppNamespaces, long, CustomerConfigurationDbContext>,
        IAppNamespacesRepository
    {
        public AppNamespacesRepository(IUnitOfWork<CustomerConfigurationDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        #region 根据appid得到应用信息

        /// <summary>
        /// 根据appid得到应用信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称空间</param>
        /// <returns></returns>
        public AppNamespaces Get(string appid, string name)
        {
            return Dbcontext.Set<AppNamespaces>().FirstOrDefault(x => x.AppId == appid && x.Name == name && !x.IsDel);
        }

        #endregion
    }
}