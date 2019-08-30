// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumeration;
using EInfrastructure.Core.HelpCommon;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// enum unit test
    /// </summary>
    public class EnumUnitTest
    {
        #region Check is Exist

        /// <summary>
        /// Check is Exist
        /// </summary>
        [Theory]
        [InlineData(1, typeof(Gender), true)]
        public void IsExist(int param, Type type, bool result)

        {
            Check.True(param.IsExist(type) == result, "not find");
        }

        #endregion

        #region check describe

        /// <summary>
        /// check describe
        /// </summary>
        public void CheckDescribe()
        {
            Check.True(Gender.Boy.Name == "男", "result is error");
        }

        #endregion

        #region Get Description

        /// <summary>
        /// Get Description
        /// </summary>
        [Fact]
        public void GetDescriptionDictionary()
        {
            var result = EnumCommon.ToDescriptionDictionary<Gender>();
        }

        #endregion
    }
}
