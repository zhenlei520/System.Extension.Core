// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Words.Config.PinYin;
using EInfrastructure.Core.Words.Config.Text;

namespace EInfrastructure.Core.Words.Config
{
    /// <summary>
    /// 路径配置
    /// </summary>
    public class EWordConfig
    {
        /// <summary>
        /// 文字拼音路径
        /// </summary>
        public DictPinYinPathConfig DictPinYinPathConfig { get; set; } = new DictPinYinPathConfig();

        /// <summary>
        /// 字典配置路径
        /// </summary>
        public DictTextPathConfig DictTextPathConfig { get; set; } = new DictTextPathConfig();
    }
}