// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Words;
using EInfrastructure.Core.Words.Config;
using EInfrastructure.Core.Words.Enum;
using EInfrastructure.Core.Words.Extension;
using EInfrastructure.Core.Words.Extension.ImportDict;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 导入词库
    /// </summary>
    public class ImportWordService : BaseWordService, IImportWordService
    {
        private readonly IWordService _wordService;

        /// <summary>
        /// 导入词库
        /// </summary>
        /// <param name="wordConfig"></param>
        /// <param name="wordService"></param>
        public ImportWordService(EWordConfig wordConfig,
            IWordService wordService) : base(wordConfig)
        {
            _wordService = wordService;
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

        #region 导入搜狗词库（导入path下的词库文件）

        /// <summary>
        /// 导入搜狗词库（导入path下的词库文件）
        /// </summary>
        public bool ImportBySouGou(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            ImportSouGouCommon.Initialize(_wordService, path);
            Reload(DictTypeEnum.PinYin);
            return true;
        }

        #endregion
    }
}
