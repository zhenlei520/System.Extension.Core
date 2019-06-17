// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enum;
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
        [Fact]
        public void IsExist()
        {
            Check.True(1.IsExist(typeof(GenderEnum)), "not find");
        }

        #endregion

        #region check describe

        /// <summary>
        /// check describe
        /// </summary>
        public void CheckDescribe()
        {
            Check.True(GenderEnum.Boy.GetDescription() == "男", "result is error");
        }

        #endregion

        #region Get Description

        /// <summary>
        /// Get Description
        /// </summary>
        [Fact]
        public void GetDictionary()
        {
            var result = EnumCommon.ToDictionary<GenderEnum>();
        }

        #endregion
        
        #region Get Description

        /// <summary>
        /// Get Description
        /// </summary>
        [Fact]
        public void GetDescriptionDictionary()
        {
            var result = EnumCommon.ToDescriptionDictionary<GenderEnum>();
        }

        #endregion
    }
}