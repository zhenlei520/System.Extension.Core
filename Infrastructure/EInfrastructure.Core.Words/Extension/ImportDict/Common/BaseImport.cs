using EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common
{
    internal class BaseImport
    {
        internal BaseImport()
        {
            DefaultRank = 0;
            CodeType = CodeTypeEnum.Pinyin;
        }
        /// <summary>
        /// 输入法编码类型
        /// </summary>
        public virtual CodeTypeEnum CodeType { get; protected set; }
        /// <summary>
        /// 默认词频，主要用于词频丢失的情况下生成词频
        /// </summary>
        public virtual int DefaultRank { get; set; }
        /// <summary>
        /// 词条总数
        /// </summary>
        public virtual int CountWord { get; set; }
        /// <summary>
        /// 当前处理了多少条
        /// </summary>
        public virtual int CurrentStatus { get; set; }
        //public virtual Form ImportConfigForm { get; private set; }
        /// <summary>
        /// 该输入法词库是不是文本词库
        /// </summary>
        public virtual bool IsText
        {
            get { return true; }
        }
    }
}