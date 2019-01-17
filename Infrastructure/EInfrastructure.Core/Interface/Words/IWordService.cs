using System.Collections.Generic;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Words.Enum;

namespace EInfrastructure.Core.Interface.Words
{
    /// <summary>
    /// 文字
    /// </summary>
    public interface IWordService : ISingleInstance
    {
        /// <summary>
        /// 得到完整的拼音
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetPinYin(string text);

        /// <summary>
        /// 得到中文简体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetSimplified(string text);

        /// <summary>
        /// 得到中文繁体
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetTraditional(string text);

        /// <summary>
        /// 得到日文
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="languageType">文字类型，默认中文简体</param>
        /// <returns></returns>
        string GetJapanese(string text, TextTypesEnum languageType = TextTypesEnum.Simplified);

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        bool IsExist(string text, int[] unicodeRange);

        /// <summary>
        /// 判断是否在范围之内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        bool IsExist(string text, List<WordTypeEnum> wordTypeList);

        /// <summary>
        /// 判断是否存在某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        bool IsExist(string text, TextTypesEnum textType);

        /// <summary>
        /// 判断是否全部在范围内
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="unicodeRange">范围</param>
        /// <returns></returns>
        bool IsAll(string text, int[] unicodeRange);

        /// <summary>
        /// 判断是否全部在范围内（至少满足一组需要全部包含）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="wordTypeList">文本类型集合</param>
        /// <returns></returns>
        bool IsAll(string text, List<WordTypeEnum> wordTypeList);

        /// <summary>
        /// 判断是否全部都是某种文字
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="textType">文字类型</param>
        /// <returns></returns>
        bool IsAll(string text, TextTypesEnum textType);
    }
}