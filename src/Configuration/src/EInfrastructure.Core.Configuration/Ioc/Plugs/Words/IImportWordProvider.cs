// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Words
{
    /// <summary>
    /// 导入词库
    /// </summary>
    public interface IImportWordProvider : ISingleInstance,IIdentify
    {
        #region 导入搜狗词库（导入path下的词库文件）

        /// <summary>
        /// 导入搜狗词库（导入path下的词库文件）
        /// </summary>
        bool ImportBySouGou(string path);

        #endregion
    }
}
