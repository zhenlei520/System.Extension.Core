// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.Files
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <param name="conditionCode">特征码</param>
        public FileInfo(string name,string conditionCode)
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
