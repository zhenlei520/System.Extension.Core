// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations.SeedWork.Configurations;

namespace EInfrastructure.Core.Configuration.Enumerations.SeedWork
{
    /// <summary>
    ///
    /// </summary>
    public abstract class Enumeration : Enumeration<int, string>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        protected Enumeration(int id, string name) : base(id, name)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="firstValue"></param>
        /// <param name="secondValue"></param>
        /// <returns></returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="enumeration"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsExist<T>(T enumeration) where T : Enumeration<int, string>
        {
            var all = GetAll<T>();
            return all.Any(x => x.Id == enumeration.Id);
        }
    }
}
