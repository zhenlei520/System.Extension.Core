// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Config.EnumerationExtensions.SeedWork.Configurations;

namespace EInfrastructure.Core.Config.EnumerationExtensions.SeedWork
{
    /// <summary>
    ///
    /// </summary>
    public abstract class Enumeration : Enumeration<int, string>
    {
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
    }
}
