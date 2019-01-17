using System.Collections.Generic;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Interface.Words;
using EInfrastructure.Core.Interface.Words.Config;
using EInfrastructure.Core.Interface.Words.Enum;

namespace EInfrastructure.Core.Words
{
    public class WordService : BaseWordService, IWordService
    {
        #region 得到完整的拼音

        /// <summary>
        /// 得到完整的拼音
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        public string GetPinYin(string text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region 得到中文简体

        /// <summary>
        /// 得到中文简体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        public string GetSimplified(string text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region 得到中文繁体

        /// <summary>
        /// 得到中文繁体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        public string GetTraditional(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++) {
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5) {
                    var k = Dict.Traditional[c - 0x4e00];
                    if (k != c) {
                        sb[i] = k;
                    }
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 得到日文

        /// <summary>
        /// 得到日文
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="languageType">语言类型，默认中文简体</param>
        /// <returns></returns>
        public string GetJapanese(string text, TextTypesEnum languageType = TextTypesEnum.Simplified)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region 判断是否存在

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        public bool IsExist(string text, int[] unicodeRange)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if ((c >= unicodeRange[0] && c <= unicodeRange[1]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        public bool IsExist(string text, List<WordTypeEnum> wordTypeList)
        {
            var wordList = UnicodeConfig.AllUnicode.Where(x => wordTypeList.Contains(x.Key)).Select(x => x.Value)
                .ToList();
            foreach (var item in wordList)
            {
                if (IsExist(text, item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否存在某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        public bool IsExist(string text, TextTypesEnum textType)
        {
            return IsExist(text, new List<WordTypeEnum>()
            {
                WordTypeEnum.BaseChinese,
                WordTypeEnum.BaseChineseExt
            });
        }

        #endregion

        #region 判断是否全部都是

        /// <summary>
        /// 判断是否全部在范围内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        public bool IsAll(string text, int[] unicodeRange)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (!(c >= unicodeRange[0] && c <= unicodeRange[1]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断是否全部在范围内（至少满足一组需要全部包含）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        public bool IsAll(string text, List<WordTypeEnum> wordTypeList)
        {
            var wordList = UnicodeConfig.AllUnicode.Where(x => wordTypeList.Contains(x.Key)).Select(x => x.Value)
                .ToList();
            foreach (var item in wordList)
            {
                if (IsAll(text, item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否全部都是某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        public bool IsAll(string text, TextTypesEnum textType)
        {
            return IsAll(text, new List<WordTypeEnum>()
            {
                WordTypeEnum.BaseChinese,
                WordTypeEnum.BaseChineseExt
            });
        }

        #endregion
    }
}