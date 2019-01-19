using System.Collections.Generic;
using System.Text;
using EInfrastructure.Core.Words.Extension;

namespace EInfrastructure.Core.Words.PinYin
{
    /// <summary>
    /// 拼音词库
    /// </summary>
    public class PinyinDict
    {
        private static WordsSearch _search;

        #region 得到文字关键词

        /// <summary>
        /// 得到文字关键词
        /// </summary>
        /// <returns></returns>
        private static WordsSearch GetWordsSearch()
        {
            if (_search == null)
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                var sp = BaseWordService.DictPinYinConfig.Word.Split(',');
                var index = 0;
                foreach (var item in sp)
                {
                    dict[item] = index;
                    index += item.Length;
                }

                _search = new WordsSearch();
                _search.SetKeywords(dict);
            }

            return _search;
        }

        #endregion

        #region 获取拼音列表

        /// <summary>
        /// 获取拼音列表
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static string[] GetPinYinList(string text)
        {
            string[] list = new string[text.Length];
            var pos = GetWordsSearch().FindAll(text);
            foreach (var p in pos)
            {
                for (int i = 0; i < p.Keyword.Length; i++)
                {
                    list[i + p.Start] =
                        BaseWordService.DictPinYinConfig.PinYinName[
                            BaseWordService.DictPinYinConfig.WordPinYin[i + p.Index]];
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (list[i] != null) continue;
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5)
                {
                    var index = c - 0x4e00;
                    var start = BaseWordService.DictPinYinConfig.PinYinIndex[index];
                    var end = BaseWordService.DictPinYinConfig.PinYinIndex[index + 1];
                    if (end > start)
                    {
                        list[i] = BaseWordService.DictPinYinConfig.PinYinName[
                            BaseWordService.DictPinYinConfig.PinYinData[start]];
                    }
                }
            }

            return list;
        }

        #endregion

        #region 得到文字全拼

        /// <summary>
        /// 得到文字全拼
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <returns></returns>
        internal static string GetPinYin(string text)
        {
            string[] list = GetPinYinList(text);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                var s = list[i];
                if (s != null)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(text[i]);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 得到文字首字母

        /// <summary>
        /// 得到文字首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static string GetFirstPinYin(string text)
        {
            string[] list = GetPinYinList(text);
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < list.Length; i++)
            {
                var c = list[i];
                if (c != null)
                {
                    sb[i] = c[0];
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 得到全部的拼音

        /// <summary>
        /// 得到全部的拼音
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static List<string> GetAllPinYin(char c)
        {
            if (c >= 0x4e00 && c <= 0x9fa5)
            {
                var index = c - 0x4e00;
                List<string> list = new List<string>();
                var start = BaseWordService.DictPinYinConfig.PinYinIndex[index];
                var end = BaseWordService.DictPinYinConfig.PinYinIndex[index + 1];
                if (end > start)
                {
                    for (int i = start; i < end; i++)
                    {
                        list.Add(BaseWordService.DictPinYinConfig.PinYinName[
                            BaseWordService.DictPinYinConfig.PinYinData[i]]);
                    }
                }

                return list;
            }

            return new List<string>();
        }

        #endregion

        #region 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]

        /// <summary>
        ///  获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static string GetPinYinFast(char c)
        {
            if (c >= 0x4e00 && c <= 0x9fa5)
            {
                var index = c - 0x4e00;
                var start = BaseWordService.DictPinYinConfig.PinYinIndex2[index];
                var end = BaseWordService.DictPinYinConfig.PinYinIndex2[index + 1];
                if (end > start)
                {
                    return BaseWordService.DictPinYinConfig.PinYinName2[
                        BaseWordService.DictPinYinConfig.PinYinData2[start]];
                }
            }

            return c.ToString();
        }

        #endregion
    }
}