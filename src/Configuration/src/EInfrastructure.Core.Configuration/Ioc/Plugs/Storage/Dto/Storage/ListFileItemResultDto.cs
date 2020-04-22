// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 文件列表
    /// </summary>
    public class ListFileItemResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <param name="msg"></param>
        public ListFileItemResultDto(bool state, string msg) : base(state, msg)
        {
        }

        /// <summary>
        /// 位置标记
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public List<FileInfoDto> Items { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<string> CommonPrefixes { get; set; }
    }
}
