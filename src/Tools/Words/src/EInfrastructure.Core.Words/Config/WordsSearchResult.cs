// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Words.Config
{
    /// <summary>
    ///
    /// </summary>
    internal class WordsSearchResult
    {
        internal WordsSearchResult(string keyword, int start, int end, int index)
        {
            Keyword = keyword;
            Success = true;
            End = end;
            Start = start;
            Index = index;
        }

        private WordsSearchResult()
        {
            Success = false;
            Start = 0;
            End = 0;
            Index = -1;
            Keyword = null;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start { get; private set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; private set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; private set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public static WordsSearchResult Empty
        {
            get { return new WordsSearchResult(); }
        }

        private int _hash = -1;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (_hash == -1)
            {
                var i = Start << 5;
                i += End - Start;
                _hash = i << 1 + (Success ? 1 : 0);
            }

            return _hash;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Start.ToString() + "|" + Keyword;
        }
    }
}
