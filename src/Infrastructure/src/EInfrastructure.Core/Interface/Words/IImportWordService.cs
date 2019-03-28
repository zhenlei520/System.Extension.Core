// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Interface.IOC;

namespace EInfrastructure.Core.Interface.Words
{
    /// <summary>
    /// 导入词库
    /// </summary>
    public interface IImportWordService : ISingleInstance
    {
        /// <summary>
        /// 导入搜狗词库（导入path下的词库文件）
        /// </summary>
        bool ImportBySouGou(string path);
    }
}