// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Words;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Words.Enumerations;
using EInfrastructure.Core.Words.Config;
using EInfrastructure.Core.Words.Extension;
using EInfrastructure.Core.Words.PinYin;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 单词帮助类
    /// </summary>
    public class WordProvider : BaseWordService, IWordProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="wordConfig"></param>
        public WordProvider(EWordConfig wordConfig) : base(wordConfig)
        {
        }

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 得到文字首字母

        /// <summary>
        /// 得到文字首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetFirstPinYin(string text)
        {
            return PinyinDict.GetFirstPinYin(text);
        }

        #endregion

        #region 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]

        /// <summary>
        /// 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        [Obsolete("请使用GetPinYin方法，此方法不支持多音")]
        public string GetPinYinFast(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                sb.Append(PinyinDict.GetPinYinFast(c));
            }

            return sb.ToString();
        }

        #endregion

        #region 得到完整的拼音

        /// <summary>
        /// 得到完整的拼音
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        public string GetPinYin(string text)
        {
            return PinyinDict.GetPinYin(text);
        }

        #endregion

        #region 获取文字的全部拼音（多读音）

        /// <summary>
        /// 获取文字的全部拼音（多读音）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        public List<string> GetAllPinYin(char text)
        {
            return PinyinDict.GetAllPinYin(text);
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
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5)
                {
                    var k = DictConfig.Simplified[c - 0x4e00];
                    if (k != c)
                    {
                        sb[i] = k;
                    }
                }
            }

            return sb.ToString();
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
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5)
                {
                    var k = DictConfig.Traditional[c - 0x4e00];
                    if (k != c)
                    {
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
        /// <returns></returns>
        public string GetJapanese(string text)
        {
            throw new NotImplementedException();
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
        public bool IsExist(string text, List<WordType> wordTypeList)
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
        public bool IsExist(string text, TextTypes textType)
        {
            if (textType.Id == TextTypes.Simplified.Id || textType.Id == TextTypes.Traditional.Id)
            {
                return IsExist(text, new List<WordType>()
                {
                    WordType.BaseChinese,
                    WordType.BaseChineseExt
                });
            }

            if (textType.Id == TextTypes.Japanese.Id)
            {
                return IsExist(text, new List<WordType>()
                {
                    WordType.JapaneseHiragana,
                    WordType.JapaneseKatakana,
                    WordType.JapaneseKatakanaSpellExt
                });
            }

            return false;
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
        public bool IsAll(string text, List<WordType> wordTypeList)
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
        public bool IsAll(string text, TextTypes textType)
        {
            return IsAll(text, new List<WordType>()
            {
                WordType.BaseChinese,
                WordType.BaseChineseExt
            });
        }

        #endregion

        #region 得到字典内容

        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <returns></returns>
        public string GetDicContent(string path)
        {
            return base.GetContent(path);
        }


        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件相对地址集合</param>
        /// <returns></returns>
        public string GetDicContent(string[] path)
        {
            return base.GetContent(path.ToList());
        }

        #endregion

        #region 数字转中文大写

        /// <summary>
        /// 数字转中文大写
        /// </summary>
        /// <param name="x">数字信息</param>
        /// <returns></returns>
        public string ToChineseRmb(double x)
        {
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s,
                @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))",
                "${b}${z}");
            return Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString());
        }

        #endregion

        #region  中文转数字（支持中文大写）

        /// <summary>
        /// 中文转数字（支持中文大写）
        /// </summary>
        /// <param name="chineseString">中文信息</param>
        /// <returns></returns>
        public decimal ToNumber(string chineseString)
        {
            return NumberConventer.ChnToArab(chineseString);
        }

        #endregion

        #region 半角 全角 转换

        #region 半角转全角

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ToSbc(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == 32)
                {
                    sb[i] = (char) 12288;
                }
                else if (c < 127)
                {
                    sb[i] = (char) (c + 65248);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 转半角的函数

        /// <summary>
        /// 转半角的函数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ToDbc(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == 12288)
                {
                    sb[i] = (char) 32;
                }
                else if (c > 65280 && c < 65375)
                {
                    sb[i] = (char) (c - 65248);
                }
            }

            return sb.ToString();
        }

        #endregion

        #endregion

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion
    }
}
