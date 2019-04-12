// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.WordsExtensions.Enum
{
    /// <summary>
    /// 文字类型
    /// </summary>
    public enum WordTypeEnum
    {
        #region 汉字

        /// <summary>
        /// 基本汉字
        /// </summary>
        [Description("基本汉字")] BaseChinese = 1,

        /// <summary>
        /// 基本汉字补充
        /// </summary>
        [Description("基本汉字补充")] BaseChineseExt = 2,

        /// <summary>
        /// 扩展A
        /// </summary>
        [Description("扩展A")] ChineseExt1 = 3,

        /// <summary>
        /// 扩展B
        /// </summary>
        [Description("扩展B")] ChineseExt2 = 4,

        /// <summary>
        /// 扩展C
        /// </summary>
        [Description("扩展C")] ChineseExt3 = 5,

        /// <summary>
        /// 扩展D
        /// </summary>
        [Description("扩展D")] ChineseExt4 = 6,

        /// <summary>
        /// 扩展E
        /// </summary>
        [Description("扩展E")] ChineseExt5 = 7,

        /// <summary>
        /// 扩展F
        /// </summary>
        [Description("扩展F")] ChineseExt6 = 8,

        /// <summary>
        /// 康熙部首
        /// </summary>
        [Description("康熙部首")] ChineseSpecialRadical = 9,

        /// <summary>
        /// 部首扩展
        /// </summary>
        [Description("部首扩展")] ChineseRadicalExt1 = 10,

        /// <summary>
        /// 兼容汉字
        /// </summary>
        [Description("兼容汉字")] ChineseCompatibility = 11,

        /// <summary>
        /// 兼容扩展
        /// </summary>
        [Description("兼容扩展")] ChineseCompatibilityExt = 12,

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        [Description("PUA(GBK)部件")] ChineseGbkPart = 13,

        /// <summary>
        /// 部件扩展
        /// </summary>
        [Description("部件扩展")] ChinesePartExt = 14,

        /// <summary>
        /// PUA增补
        /// </summary>
        [Description("PUA增补")] ChinesePuaExt = 15,

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        [Description("PUA(GBK)部件")] ChineseStroke = 16,

        /// <summary>
        /// 汉字结构
        /// </summary>
        [Description("汉字结构")] ChineseStructure = 17,

        /// <summary>
        /// 汉语注音
        /// </summary>
        [Description("汉语注音")] ChinesePhoneticNotation = 18,

        /// <summary>
        /// 注音扩展
        /// </summary>
        [Description("注音扩展")] ChinesePhoneticNotationExt = 19,

        /// <summary>
        ///  特殊 1个字
        /// </summary>
        [Description("特殊")] ChineseSpecial = 20,

        #endregion

        #region 日文

        /// <summary>
        /// 日文平假名
        /// </summary>
        [Description("日文平假名")] JapaneseHiragana = 21,

        /// <summary>
        /// 日文片假名
        /// </summary>
        [Description("日文片假名")] JapaneseKatakana = 22,

        /// <summary>
        /// 日文片假名拼音扩展
        /// </summary>
        [Description("日文片假名拼音扩展")] JapaneseKatakanaSpellExt = 23,

        #endregion

        #region 韩文

        /// <summary>
        /// 韩文拼音
        /// </summary>
        [Description("韩文拼音")] KoreanAlphabet = 24,

        /// <summary>
        /// 韩文字母
        /// </summary>
        [Description("韩文字母")] KoreanLetters = 25,

        /// <summary>
        /// 韩文兼容字母
        /// </summary>
        [Description("韩文兼容字母")] KoreanLettersExt = 26,

        #endregion

        #region 特殊符号

        /// <summary>
        /// 中文竖排标点
        /// </summary>
        [Description("")] ChineseVerticalPunctuation = 27,

        /// <summary>
        /// CJK特殊符号（日期合并）
        /// </summary>
        [Description("CJK特殊符号（日期合并）")] SpecialSymbols = 28,

        /// <summary>
        /// 装饰符号（非CJK专用）
        /// </summary>
        [Description("装饰符号（非CJK专用）")] DecorativeSymbols = 29,

        /// <summary>
        /// 杂项符号（非CJK专用）
        /// </summary>
        [Description("杂项符号（非CJK专用）")] MiscellaneousSymbols = 30,

        /// <summary>
        /// CJK兼容符号（竖排变体、下划线、顿号）
        /// </summary>
        [Description("CJK兼容符号（竖排变体、下划线、顿号）")] CompatibilitySymbols = 31,

        /// <summary>
        /// CJK字母及月份
        /// </summary>
        [Description("CJK字母及月份")] LettersAndMonths = 32

        #endregion
    }
}