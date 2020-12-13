// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfos
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <param name="conditionCode">特征码</param>
        public FileInfos(string name,string conditionCode)
        {
            Name = name;
            ConditionCode = conditionCode;
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get;  }

        /// <summary>
        /// 特征码
        /// </summary>
        public string ConditionCode { get;  }
    }
}
