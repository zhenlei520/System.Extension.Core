using System.Collections.Generic;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common.Entities
{
    /// <summary>
    ///     词库类，含有多个词条
    /// </summary>
    internal class WordLibraryList : List<WordLibrary>
    {
        /// <summary>
        ///     将词库中重复出现的单词合并成一个词，多词库合并时使用(词重复就算)
        /// </summary>
        internal void MergeSameWord()
        {
            var dic = new Dictionary<string, WordLibrary>();
            foreach (WordLibrary wl in this)
            {
                if (!dic.ContainsKey(wl.Word))
                {
                    dic.Add(wl.Word, wl);
                }
            }
            Clear();
            foreach (WordLibrary wl in dic.Values)
            {
                Add(wl);
            }
        }

        public void AddWordLibraryList(WordLibraryList wll)
        {
            if (wll != null)
            {
                AddRange(wll);
            }
        }
    }
}