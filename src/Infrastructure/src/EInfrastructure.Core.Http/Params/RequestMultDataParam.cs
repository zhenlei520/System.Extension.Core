// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Http.Params
{
    /// <summary>
    ///
    /// </summary>
    public class RequestMultDataParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileByte">文件数组</param>
        /// <param name="contextType">文件类型</param>
        public RequestMultDataParam(string name, string fileName,byte[] fileByte, string contextType)
        {
            Name = name;
            FileName = fileName;
            FileByte = fileByte;
            ContextType = contextType;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 文件字节流
        /// </summary>
        public byte[] FileByte { get; }

        /// <summary>
        /// 文本类型
        /// </summary>
        public string ContextType { get; }
    }
}
