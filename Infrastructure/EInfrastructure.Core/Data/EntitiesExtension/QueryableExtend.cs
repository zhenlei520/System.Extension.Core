using System.Linq;

namespace EInfrastructure.Core.Data.EntitiesExtension
{
    public static class QueryableExtend
    {
        #region 返回IQueryable<T>前几条数据
        /// <summary>
        /// 返回IQueryable前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public static IQueryable<T> TopN<T>(this IQueryable<T> query, int topN)
        {
            return query.Take(topN);
        }
        #endregion

        #region 对IQueryable<T>进行分页
        /// <summary>
        /// 对IQueryable进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public static IQueryable<T> QueryPager<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            if (pageSize > 0)
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
        #endregion

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

        #region 得到IQueryable<T>的分页后数据源
        /// <summary>
        /// 得到IQueryable的分页后数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize">每页几条数据</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="isTatal">是否统计总页数</param>
        /// <returns></returns>
        public static PageData<T> QueryPager<T>(this IQueryable<T> query, int pageSize, int pageIndex, bool isTatal) where T : class, new()
        {
            PageData<T> list = new PageData<T>();

            if (isTatal)
            {
                list.RowCount = query.Count();
            }

            list.Data = query.QueryPager(pageSize, pageIndex).ToList();

            return list;
        }
        #endregion

    }
}
