// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Ioc;

namespace EInfrastructure.Core.Configuration
{
    /// <summary>
        /// 比较集合
        /// </summary>
        public class ListCompare<T, TKey> where T : IEntity<TKey>
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
            public List<T> CreateList => _createList ?? (_createList =
                OptList.Where(x => !
                        SourceList.Any(source => source.Id.Equals(x.Id)))
                    .ToList());

            #endregion

            #region 更新列表

            private List<T> _updateList;

            /// <summary>
            /// 更新列表
            /// </summary>
            public List<T> UpdateList => _updateList ??
                                         (_updateList = OptList.Where(opt =>
                                                 SourceList.Any(source => source.Id.Equals(opt.Id)))
                                             .ToList());

            #endregion

            #region 删除列表

            private List<T> _deleteList;

            /// <summary>
            /// 删除列表
            /// </summary>
            public List<T> DeleteList => _deleteList ??
                                         (_deleteList =
                                             SourceList.Where(source => !
                                                     OptList.Any(opt => opt.Id.Equals(source.Id)))
                                                 .ToList());

            #endregion
        }
}
