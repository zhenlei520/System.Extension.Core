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
        /// 得到文字首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GetFirstPinYin(string text);

        /// <summary>
        /// 得到完整的拼音
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetPinYin(string text);

        /// <summary>
        /// 获取拼音全拼, 不支持多音,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        string GetPinYinFast(string text);

        /// <summary>
        /// 获取文字的全部拼音（多读音）
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <returns></returns>
        List<string> GetAllPinYin(char text);

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
        /// <returns></returns>
        string GetJapanese(string text);

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

        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <returns></returns>
        string GetDicContent(string path);

        /// <summary>
        /// 得到字典内容
        /// </summary>
        /// <param name="path">文件相对地址集合</param>
        /// <returns></returns>
        string GetDicContent(string[] path);

        /// <summary>
        /// 数字转中文大写
        /// </summary>
        /// <param name="x">数字信息</param>
        /// <returns></returns>
        string ToChineseRmb(double x);

        /// <summary>
        /// 中文转数字（支持中文大写）
        /// </summary>
        /// <param name="chineseString">中文信息</param>
        /// <returns></returns>
        decimal ToNumber(string chineseString);

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ToSbc(string input);

        /// <summary>
        /// 转半角的函数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ToDbc(string input);
    }
}