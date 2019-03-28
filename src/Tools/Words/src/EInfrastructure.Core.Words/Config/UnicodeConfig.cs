// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

/**
 * 详细查看https://www.qqxiuzi.cn/zh/hanzi-unicode-bianma.php
 */

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Interface.Words.Enum;

namespace EInfrastructure.Core.Words.Config
{
    /// <summary>
    /// Unicode编码范围
    /// </summary>
    public class UnicodeConfig
    {
        #region 所有的编码范围

        /// <summary>
        /// 所有的编码范围
        /// </summary>
        public static List<KeyValuePair<WordTypeEnum, int[]>> AllUnicode = new List<KeyValuePair<WordTypeEnum, int[]>>()
        {
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.BaseChinese, BaseChinese),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.BaseChineseExt, BaseChineseExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt1, ChineseExt1),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt2, ChineseExt2),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt3, ChineseExt3),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt4, ChineseExt4),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt5, ChineseExt5),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseExt6, ChineseExt6),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseSpecialRadical, ChineseSpecialRadical),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseRadicalExt1, ChineseRadicalExt1),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseCompatibility, ChineseCompatibility),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseCompatibilityExt, ChineseCompatibilityExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseGbkPart, ChineseGbkPart),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChinesePartExt, ChinesePartExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChinesePuaExt, ChinesePuaExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseStroke, ChineseStroke),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseStructure, ChineseStructure),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChinesePhoneticNotation, ChinesePhoneticNotation),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChinesePhoneticNotationExt, ChinesePhoneticNotationExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseSpecial, ChineseSpecial),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.JapaneseHiragana, JapaneseHiragana),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.JapaneseKatakana, JapaneseKatakana),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.JapaneseKatakanaSpellExt, JapaneseKatakanaSpellExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.KoreanAlphabet, KoreanAlphabet),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.KoreanLetters, KoreanLetters),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.KoreanLettersExt, KoreanLettersExt),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.ChineseVerticalPunctuation, ChineseVerticalPunctuation),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.SpecialSymbols, SpecialSymbols),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.DecorativeSymbols, DecorativeSymbols),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.MiscellaneousSymbols, MiscellaneousSymbols),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.CompatibilitySymbols, CompatibilitySymbols),
            new KeyValuePair<WordTypeEnum, int[]>(WordTypeEnum.LettersAndMonths, LettersAndMonths)
        };

        #endregion

        #region 汉字

        /// <summary>
        /// 基本汉字
        /// </summary>
        public static int[] BaseChinese = new int[2] {Convert.ToInt32("4E00", 16), Convert.ToInt32("9FA5", 16)};

        /// <summary>
        /// 基本汉字补充
        /// </summary>
        public static int[] BaseChineseExt = new int[2] {Convert.ToInt32("9FA6", 16), Convert.ToInt32("9FEF", 16)};

        /// <summary>
        /// 扩展A
        /// </summary>
        public static int[] ChineseExt1 = new int[2] {Convert.ToInt32("3400", 16), Convert.ToInt32("4DB5", 16)};

        /// <summary>
        /// 扩展B
        /// </summary>
        public static int[] ChineseExt2 = new int[2] {Convert.ToInt32("20000", 16), Convert.ToInt32("2A6D6", 16)};

        /// <summary>
        /// 扩展C
        /// </summary>
        public static int[] ChineseExt3 = new int[2] {Convert.ToInt32("2A700", 16), Convert.ToInt32("2B734", 16)};

        /// <summary>
        /// 扩展D
        /// </summary>
        public static int[] ChineseExt4 = new int[2] {Convert.ToInt32("2B740", 16), Convert.ToInt32("2B81D", 16)};

        /// <summary>
        /// 扩展E
        /// </summary>
        public static int[] ChineseExt5 = new int[2] {Convert.ToInt32("2B820", 16), Convert.ToInt32("2CEA1", 16)};

        /// <summary>
        /// 扩展F
        /// </summary>
        public static int[] ChineseExt6 = new int[2] {Convert.ToInt32("2CEB0", 16), Convert.ToInt32("2EBE0", 16)};

        /// <summary>
        /// 康熙部首
        /// </summary>
        public static int[] ChineseSpecialRadical =
            new int[2] {Convert.ToInt32("2F00", 16), Convert.ToInt32("2FD5", 16)};

        /// <summary>
        /// 部首扩展
        /// </summary>
        public static int[] ChineseRadicalExt1 = new int[2] {Convert.ToInt32("2E80", 16), Convert.ToInt32("2EF3", 16)};

        /// <summary>
        /// 兼容汉字
        /// </summary>
        public static int[] ChineseCompatibility =
            new int[2] {Convert.ToInt32("F900", 16), Convert.ToInt32("FAD9", 16)};

        /// <summary>
        /// 兼容扩展
        /// </summary>
        public static int[] ChineseCompatibilityExt =
            new int[2] {Convert.ToInt32("2F800", 16), Convert.ToInt32("2FA1D", 16)};

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        public static int[] ChineseGbkPart = new int[2] {Convert.ToInt32("E815", 16), Convert.ToInt32("E86F", 16)};

        /// <summary>
        /// 部件扩展
        /// </summary>
        public static int[] ChinesePartExt = new int[2] {Convert.ToInt32("E400", 16), Convert.ToInt32("E5E8", 16)};

        /// <summary>
        /// PUA增补
        /// </summary>
        public static int[] ChinesePuaExt = new int[2] {Convert.ToInt32("E600", 16), Convert.ToInt32("E6CF", 16)};

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        public static int[] ChineseStroke = new int[2] {Convert.ToInt32("31C0", 16), Convert.ToInt32("31E3", 16)};

        /// <summary>
        /// 汉字结构
        /// </summary>
        public static int[] ChineseStructure = new int[2] {Convert.ToInt32("2FF0", 16), Convert.ToInt32("2FFB", 16)};

        /// <summary>
        /// 汉语注音
        /// </summary>
        public static int[] ChinesePhoneticNotation =
            new int[2] {Convert.ToInt32("3105", 16), Convert.ToInt32("312F", 16)};

        /// <summary>
        /// 注音扩展
        /// </summary>
        public static int[] ChinesePhoneticNotationExt =
            new int[2] {Convert.ToInt32("31A0", 16), Convert.ToInt32("31BA", 16)};

        /// <summary>
        /// 特殊 1个字 〇
        /// </summary>
        public static int[] ChineseSpecial = new int[2] {Convert.ToInt32("3007", 16), Convert.ToInt32("3007", 16)};

        #endregion

        #region 日文

        /// <summary>
        /// 日文平假名
        /// </summary>
        public static int[] JapaneseHiragana = new int[2] {Convert.ToInt32("3040", 16), Convert.ToInt32("309F", 16)};

        /// <summary>
        /// 日文片假名
        /// </summary>
        public static int[] JapaneseKatakana = new int[2] {Convert.ToInt32("30A0", 16), Convert.ToInt32("30FF", 16)};

        /// <summary>
        /// 日文片假名拼音扩展
        /// </summary>
        public static int[] JapaneseKatakanaSpellExt =
            new int[2] {Convert.ToInt32("31F0", 16), Convert.ToInt32("31FF", 16)};

        #endregion

        #region 韩文

        /// <summary>
        /// 韩文拼音
        /// </summary>
        public static int[] KoreanAlphabet = new int[2] {Convert.ToInt32("AC00", 16), Convert.ToInt32("D7AF", 16)};

        /// <summary>
        /// 韩文字母
        /// </summary>
        public static int[] KoreanLetters = new int[2] {Convert.ToInt32("1100", 16), Convert.ToInt32("11FF", 16)};

        /// <summary>
        /// 韩文兼容字母
        /// </summary>
        public static int[] KoreanLettersExt = new int[2] {Convert.ToInt32("3130", 16), Convert.ToInt32("318F", 16)};

        #endregion

        #region 特殊符号

        /// <summary>
        /// 中文竖排标点
        /// </summary>
        public static int[] ChineseVerticalPunctuation =
            new int[2] {Convert.ToInt32("FE10", 16), Convert.ToInt32("FE1F", 16)};

        /// <summary>
        /// CJK特殊符号（日期合并）
        /// </summary>
        public static int[] SpecialSymbols = new int[2] {Convert.ToInt32("3300", 16), Convert.ToInt32("33FF", 16)};

        /// <summary>
        /// 装饰符号（非CJK专用）
        /// </summary>
        public static int[] DecorativeSymbols = new int[2] {Convert.ToInt32("2700", 16), Convert.ToInt32("27BF", 16)};

        /// <summary>
        /// 杂项符号（非CJK专用）
        /// </summary>
        public static int[] MiscellaneousSymbols =
            new int[2] {Convert.ToInt32("2600", 16), Convert.ToInt32("26FF", 16)};

        /// <summary>
        /// CJK兼容符号（竖排变体、下划线、顿号）
        /// </summary>
        public static int[] CompatibilitySymbols =
            new int[2] {Convert.ToInt32("FE30", 16), Convert.ToInt32("FE4F", 16)};

        /// <summary>
        /// CJK字母及月份
        /// </summary>
        public static int[] LettersAndMonths = new int[2] {Convert.ToInt32("3200", 16), Convert.ToInt32("32FF", 16)};

        #endregion
    }
}