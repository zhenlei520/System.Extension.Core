// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.Config.Entities.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class QueryableExtend
    {
        #region 得到IQueryable<T>的分页后实体集合

        /// <summary>
        /// 得到IQueryable的分页后实体集合
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="isTotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageData<T> ListPager<T>(this IQueryable<T> query, int pageSize, int pageIndex, bool isTotal)
        {
            PageData<T> list = new PageData<T>();

            if (isTotal)
            {
                list.RowCount = query.Count();
            }

            list.Data = query.QueryPager(pageSize, pageIndex).ToList();

            return list;
        }

        #endregion
    }
}
