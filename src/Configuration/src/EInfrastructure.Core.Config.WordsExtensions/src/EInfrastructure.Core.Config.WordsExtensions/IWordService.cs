// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Config.WordsExtensions.Enum;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Config.WordsExtensions
{
    /// <summary>
    /// 文字
    /// </summary>
    public interface IWordService : ISingleInstance, IIdentify
    {
        #region 得到文字首字母

        /// <summary>
        /// 得到文字首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GetFirstPinYin(string text);

        #endregion

        #region 得到完整的拼音

        /// <summary>
        /// 得到完整的拼音
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetPinYin(string text);

        #endregion

        #region 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]

        /// <summary>
        /// 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetPinYinFast(string text);

        #endregion

        #region 获取文字的全部拼音（多读音）

        /// <summary>
        /// 获取文字的全部拼音（多读音）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        List<string> GetAllPinYin(char text);

        #endregion

        #region 得到中文简体

        /// <summary>
        /// 得到中文简体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetSimplified(string text);

        #endregion

        #region 得到中文繁体

        /// <summary>
        /// 得到中文繁体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetTraditional(string text);

        #endregion

        #region 得到日文

        /// <summary>
        /// 得到日文
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetJapanese(string text);

        #endregion

        #region 判断是否在范围之内

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        bool IsExist(string text, int[] unicodeRange);

        #endregion

        #region 判断是否在范围之内

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        bool IsExist(string text, List<WordTypeEnum> wordTypeList);

        #endregion

        #region 判断是否存在某种文字

        /// <summary>
        /// 判断是否存在某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        bool IsExist(string text, TextTypesEnum textType);

        #endregion

        #region 判断是否全部在范围内

        /// <summary>
        /// 判断是否全部在范围内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        bool IsAll(string text, int[] unicodeRange);

        #endregion

        #region 判断是否全部在范围内（至少满足一组需要全部包含）

        /// <summary>
        /// 判断是否全部在范围内（至少满足一组需要全部包含）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        bool IsAll(string text, List<WordTypeEnum> wordTypeList);

        #endregion

        #region 判断是否全部都是某种文字

        /// <summary>
        /// 判断是否全部都是某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        bool IsAll(string text, TextTypesEnum textType);

        #endregion

        #region 得到字典内容

        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <returns></returns>
        string GetDicContent(string path);

        #endregion

        #region 得到字典内容

        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件相对地址集合</param>
        /// <returns></returns>
        string GetDicContent(string[] path);

        #endregion

        #region 数字转中文大写

        /// <summary>
        /// 数字转中文大写
        /// </summary>
        /// <param name="x">数字信息</param>
        /// <returns></returns>
        string ToChineseRmb(double x);

        #endregion

        #region 中文转数字（支持中文大写）

        /// <summary>
        /// 中文转数字（支持中文大写）
        /// </summary>
        /// <param name="chineseString">中文信息</param>
        /// <returns></returns>
        decimal ToNumber(string chineseString);

        #endregion

        #region 半角转全角

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ToSbc(string input);

        #endregion

        #region 转半角的函数

        /// <summary>
        /// 转半角的函数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ToDbc(string input);

        #endregion
    }
}
