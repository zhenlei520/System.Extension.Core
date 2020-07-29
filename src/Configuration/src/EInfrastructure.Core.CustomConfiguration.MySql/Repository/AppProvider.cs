// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
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
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.CustomConfiguration.MySql.Repository
{
    /// <summary>
    /// 
    /// </summary>
    internal class AppProvider : IAppProvider
    {
        private readonly IAppRepository _repository;
        private readonly IQuery<Apps, long, CustomerConfigurationDbContext> _query;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="query"></param>
        internal AppProvider(IAppRepository repository,
            IQuery<Apps, long, CustomerConfigurationDbContext> query)
        {
            this._repository = repository;
            this._query = query;
        }

        #region 添加应用

        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="name">应用名称</param>
        public void Add(string appid, string name)
        {
            CheckAppid(appid);
            CheckName(name);
            Apps apps = this._repository.Get(appid);
            if (apps != null)
            {
                throw new BusinessException(appid + "已存在");
            }

            apps = new Apps(appid, name);
            this._repository.Add(apps);
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 删除应用

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appid">应用id</param>
        public void Del(string appid)
        {
            var apps = this._repository.Get(appid);
            apps?.Del();
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 更新应用

        /// <summary>
        /// 更新应用
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="param">新应用信息</param>
        public void Update(string appid, EditAppParam param)
        {
            new EditAppParam.EditAppParamValidator().Validate(param).Check(HttpStatus.Err.Name);
            var apps = this._repository.Get(appid);
            if (apps == null)
            {
                throw new BusinessException("当前应用不存在或者已经被删除");
            }

            if (this._query.GetQueryable().Any(x => x.Id != apps.Id && !x.IsDel && x.AppId == param.Appid))
            {
                throw new BusinessException("当前appid已存在，修改失败");
            }

            apps.Update(param.Appid, param.Name);
            this._repository.UnitOfWork.Commit();
        }

        #endregion

        #region 得到应用集合

        /// <summary>
        /// 得到应用集合
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PageData<AppItemDto> GetList(string keyword, int pageIndex, int pageSize)
        {
            Expression<Func<Apps, bool>> condition = x => !x.IsDel;
            if (!string.IsNullOrEmpty(keyword))
            {
                condition = condition.And(x => x.Name.Contains(keyword));
            }

            return _query.GetQueryable().Where(condition).Select(x => new AppItemDto()
            {
                Id = x.Id,
                AppId = x.AppId,
                Name = x.Name,
                EditTime = x.EditTime
            }).ListPager(pageSize, pageIndex, true);
        }

        #endregion

        #region 得到应用信息

        /// <summary>
        /// 得到应用信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public AppDetailDto Get(string appid)
        {
            return _query.GetQueryable().Where(x => x.AppId == appid && !x.IsDel).Select(x => new AppDetailDto()
            {
                Id = x.Id,
                AppId = x.AppId,
                Name = x.Name,
                AddTime = x.AddTime,
                EditTime = x.EditTime,
            }).FirstOrDefault();
        }

        #endregion

        #region private methods

        #region 检测信息

        /// <summary>
        /// 检测信息
        /// </summary>
        /// <param name="appid">应用id</param>
        void CheckAppid(string appid)
        {
            if (string.IsNullOrEmpty(appid))
            {
                throw new ArgumentNullException(nameof(appid));
            }

            if (appid.Length > 50)
            {
                throw new BusinessException("the appid length is less than or equal to 50");
            }
        }

        #endregion

        #region 检测信息

        /// <summary>
        /// 检测信息
        /// </summary>
        /// <param name="name">应用名称</param>
        void CheckName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.Length > 50)
            {
                throw new BusinessException("the name length is less than or equal to 50");
            }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 应用仓储
    /// </summary>
    public interface IAppRepository : IRepository<Apps, long, CustomerConfigurationDbContext>, IPerRequest
    {
        /// <summary>
        /// 根据appid得到应用信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        Apps Get(string appid);
    }

    /// <summary>
    /// 应用仓储
    /// </summary>
    public class AppRepository : RepositoryBase<Apps, long, CustomerConfigurationDbContext>, IAppRepository
    {
        public AppRepository(IUnitOfWork<CustomerConfigurationDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        #region 根据appid得到应用信息

        /// <summary>
        /// 得到应用信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public Apps Get(string appid)
        {
            return Dbcontext.Set<Apps>().FirstOrDefault(x => x.AppId == appid && !x.IsDel);
        }

        #endregion
    }
}