using System.ComponentModel;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum
{
    /// <summary>
    /// 输入法类型
    /// </summary>
    internal enum CodeTypeEnum
    {
        /// <summary>
        /// 用户自定义短语
        /// </summary>
        [Description("")] UserDefinePhrase,

        /// <summary>
        /// 五笔86
        /// </summary>
        [Description("")] Wubi,

        /// <summary>
        /// 五笔98
        /// </summary>
        [Description("五笔98")] Wubi98,

        /// <summary>
        /// 郑码
        /// </summary>
        [Description("")] Zhengma,

        /// <summary>
        /// 仓颉
        /// </summary>
        [Description("仓颉")] Cangjie,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] Unknown,

        /// <summary>
        /// 用户自定义
        /// </summary>
        [Description("用户自定义")] UserDefine,

        /// <summary>
        /// 拼音
        /// </summary>
        [Description("拼音")] Pinyin,

        /// <summary>
        /// 永码
        /// </summary>
        [Description("永码")] Yong,

        /// <summary>
        /// 青松二笔
        /// </summary>
        [Description("青松二笔")] QingsongErbi,

        /// <summary>
        /// 超强二笔30键
        /// </summary>
        [Description("超强二笔30键")] ChaoqiangErbi,

        /// <summary>
        /// 超强音形(二笔)
        /// </summary>
        [Description("超强音形(二笔)")] ChaoqingYinxin,

        /// <summary>
        /// 英语
        /// </summary>
        [Description("英语")] English,

        /// <summary>
        /// 内码
        /// </summary>
        [Description("内码")] InnerCode,

        /// <summary>
        /// 现代二笔
        /// </summary>
        [Description("现代二笔")] XiandaiErbi,

        /// <summary>
        /// 注音
        /// </summary>
        [Description("注音")] Zhuyin,

        /// <summary>
        /// 地球拼音
        /// </summary>
        [Description("地球拼音")] TerraPinyin,

        /// <summary>
        /// 超音速写
        /// </summary>
        [Description("超音速写")] Chaoyin,

        /// <summary>
        /// 无编码
        /// </summary>
        [Description("无编码")] NoCode
    }
}