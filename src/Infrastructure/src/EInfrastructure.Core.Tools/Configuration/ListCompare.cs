// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// 比较集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListCompare<T> where T : struct
    {
        /// <summary>
        /// 初始化列表比较结果
        /// </summary>
        /// <param name="sourceList">原列表</param>
        /// <param name="optList">新列表</param>
        public ListCompare(List<T> sourceList, List<T> optList)
        {
            SourceList = sourceList ?? new List<T>();
            OptList = optList ?? new List<T>();
        }

        /// <summary>
        /// 原列表
        /// </summary>
        private IEnumerable<T> SourceList { get; }

        /// <summary>
        /// 新列表
        /// </summary>
        private IEnumerable<T> OptList { get; }

        #region 创建列表

        private List<T> _createList;

        /// <summary>
        /// 创建列表
        /// </summary>
        public List<T> CreateList
        {
            get
            {
                if (_createList == null)
                {
                    _createList =
                        OptList.ExceptNew(SourceList);
                }

                return _createList;
            }
        }

        #endregion

        #region 更新列表

        private List<T> _updateList;

        /// <summary>
        /// 更新列表
        /// </summary>
        public List<T> UpdateList
        {
            get
            {
                if (_updateList == null)
                {
                    _updateList = OptList.Where(opt =>
                            SourceList.Any(source => source.Equals(opt)))
                        .ToList();
                }

                return _updateList;
            }
        }

        #endregion

        #region 删除列表

        private List<T> _deleteList;

        /// <summary>
        /// 删除列表
        /// </summary>
        public List<T> DeleteList
        {
            get
            {
                if (_deleteList == null)
                {
                    _deleteList =
                        SourceList.ExceptNew(OptList);
                }

                return _deleteList;
            }
        }

        #endregion
    }
}
