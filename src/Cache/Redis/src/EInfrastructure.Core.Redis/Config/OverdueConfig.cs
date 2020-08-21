// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Redis.Config
{
    /// <summary>
    /// 滑动配置
    /// </summary>
    public class OverdueConfig<T>
    {
        /// <summary>
        ///
        /// </summary>
        public OverdueConfig()
        {
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        public OverdueConfig(T data) : this()
        {
            this.Data = data;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public T Data { get; private set; }
    }
}
