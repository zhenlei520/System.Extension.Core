// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Config.WordsExtensions.Enumeration
{
    /// <summary>
    /// 文字类型
    /// </summary>
    public class WordType : EInfrastructure.Core.Configuration.SeedWork.Enumeration
    {
        #region 汉字

        /// <summary>
        /// 基本汉字
        /// </summary>
        public static WordType BaseChinese = new WordType(1, "基本汉字");

        /// <summary>
        /// 基本汉字补充
        /// </summary>
        public static WordType BaseChineseExt = new WordType(2, "基本汉字补充");

        /// <summary>
        /// 扩展A
        /// </summary>
        public static WordType ChineseExt1 = new WordType(3, "扩展A");

        /// <summary>
        /// 扩展B
        /// </summary>
        public static WordType ChineseExt2 = new WordType(4, "扩展B");

        /// <summary>
        /// 扩展C
        /// </summary>
        public static WordType ChineseExt3 = new WordType(5, "扩展C");

        /// <summary>
        /// 扩展D
        /// </summary>
        public static WordType ChineseExt4 = new WordType(6, "扩展D");

        /// <summary>
        /// 扩展E
        /// </summary>
        public static WordType ChineseExt5 = new WordType(7, "扩展E");

        /// <summary>
        /// 扩展F
        /// </summary>
        public static WordType ChineseExt6 = new WordType(8, "扩展F");

        /// <summary>
        /// 康熙部首
        /// </summary>
        public static WordType ChineseSpecialRadical = new WordType(9, "康熙部首");

        /// <summary>
        /// 部首扩展
        /// </summary>
        public static WordType ChineseRadicalExt1 = new WordType(10, "部首扩展");

        /// <summary>
        /// 兼容汉字
        /// </summary>
        public static WordType ChineseCompatibility = new WordType(11, "兼容汉字");

        /// <summary>
        /// 兼容扩展
        /// </summary>
        public static WordType ChineseCompatibilityExt = new WordType(12, "兼容扩展");

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        public static WordType ChineseGbkPart = new WordType(13, "PUA(GBK)部件");

        /// <summary>
        /// 部件扩展
        /// </summary>
        public static WordType ChinesePartExt = new WordType(14, "部件扩展");

        /// <summary>
        /// PUA增补
        /// </summary>
        public static WordType ChinesePuaExt = new WordType(15, "PUA增补");

        /// <summary>
        /// PUA(GBK)部件
        /// </summary>
        public static WordType ChineseStroke = new WordType(16, "PUA(GBK)部件");

        /// <summary>
        /// 汉字结构
        /// </summary>
        public static WordType ChineseStructure = new WordType(17, "汉字结构");

        /// <summary>
        /// 汉语注音
        /// </summary>
        public static WordType ChinesePhoneticNotation = new WordType(18, "汉语注音");

        /// <summary>
        /// 注音扩展
        /// </summary>
        public static WordType ChinesePhoneticNotationExt = new WordType(19, "注音扩展");

        /// <summary>
        /// 特殊
        /// </summary>
        public static WordType ChineseSpecial = new WordType(20, "特殊");

        #endregion

        #region 日文

        /// <summary>
        ///日文平假名
        /// </summary>
        public static WordType JapaneseHiragana = new WordType(21, "日文平假名");

        /// <summary>
        /// 日文片假名
        /// </summary>
        public static WordType JapaneseKatakana = new WordType(22, "日文片假名");

        /// <summary>
        /// 日文片假名拼音扩展
        /// </summary>
        public static WordType JapaneseKatakanaSpellExt = new WordType(23, "日文片假名拼音扩展");

        #endregion

        #region 韩文

        /// <summary>
        /// 韩文拼音
        /// </summary>
        public static WordType KoreanAlphabet = new WordType(24, "韩文拼音");

        /// <summary>
        /// 韩文字母
        /// </summary>
        public static WordType KoreanLetters=new WordType(25,"韩文字母");

        /// <summary>
        /// 韩文兼容字母
        /// </summary>
        public static WordType KoreanLettersExt=new WordType(26,"韩文兼容字母");

        #endregion

        #region 特殊符号

        /// <summary>
        /// 中文竖排标点
        /// </summary>
        public static WordType ChineseVerticalPunctuation = new WordType(27, "中文竖排标点");

        /// <summary>
        /// CJK特殊符号（日期合并）
        /// </summary>
        public static WordType SpecialSymbols = new WordType(28, "CJK特殊符号（日期合并）");

        /// <summary>
        /// 装饰符号（非CJK专用）
        /// </summary>
        public static WordType DecorativeSymbols = new WordType(29, "装饰符号（非CJK专用）");

        /// <summary>
        /// 杂项符号（非CJK专用）
        /// </summary>
        public static WordType MiscellaneousSymbols = new WordType(30, "杂项符号（非CJK专用）");

        /// <summary>
        /// CJK兼容符号（竖排变体、下划线、顿号）
        /// </summary>
        public static WordType CompatibilitySymbols = new WordType(31, "CJK兼容符号（竖排变体、下划线、顿号）");

        /// <summary>
        /// CJK字母及月份
        /// </summary>
        public static WordType LettersAndMonths = new WordType(32, "CJK字母及月份");

        #endregion

        public WordType(int id, string name) : base(id, name)
        {
        }
    }
}
