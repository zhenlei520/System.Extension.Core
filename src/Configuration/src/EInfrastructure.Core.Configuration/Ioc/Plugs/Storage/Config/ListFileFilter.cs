// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config
{
    /// <summary>
    /// 文件列表筛选
    /// </summary>
    public class ListFileFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="delimiter">指定目录分隔符，列出所有公共前缀（模拟列出目录效果）</param>
        /// <param name="lastMark">最后一次访问的文件</param>
        /// <param name="pageSize">每页数量（默认1000条）</param>
        public ListFileFilter(string prefix, string delimiter, string lastMark, int pageSize = 1000)
        {
            Prefix = prefix;
            Delimiter = delimiter;
            LastMark = lastMark;
            PageSize = pageSize;
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// 指定目录分隔符，列出所有公共前缀（模拟列出目录效果）
        /// </summary>
        public string Delimiter { get; private set; }

        /// <summary>
        /// 最后一次访问的文件
        /// </summary>
        public string LastMark { get; private set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; private set; }
    }
}
