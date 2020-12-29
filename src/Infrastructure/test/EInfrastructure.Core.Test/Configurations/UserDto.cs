// Copyright (c) zhenlei520 All rights reserved.

using System;

namespace EInfrastructure.Core.Test.Configurations
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserDto : IComparable
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// 0 女，1男，2：未知
        /// </summary>
        public short Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return this.Id - ((UserDto) obj).Id;
        }
    }
}
