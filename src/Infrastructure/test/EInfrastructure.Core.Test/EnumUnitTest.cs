// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
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
        [InlineData(1, typeof(GenderEnum), true)]
        public void IsExist(int param, Type type, bool result)

        {
            Check.True(param.IsExist(type) == result, "not find");
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

        #region GetList

        /// <summary>
        /// GetList
        /// </summary>
        [Fact]
        public void GetKeys()
        {
            var result = EnumCommon.GetKeys<GenderEnum>();
        }

        /// <summary>
        /// GetList
        /// </summary>
        [Fact]
        public void GetValues()
        {
            var result = EnumCommon.GetValues<GenderEnum>();
        }

        #endregion


        /// <summary>
        /// GetList
        /// </summary>
        [Fact]
        public void ToEnumAndAttributes()
        {
            var res = EnumCommon.ToEnumAndAttributes<ENameAttribute>(typeof(GenderEnum))
                .Where(x => x.Value.Name.Contains("男"))
                .Select(x => x.Key).FirstOrDefault();
            Check.True(res != null && (GenderEnum) res == GenderEnum.Boy, "参数错误");

            var str2 = GenderEnum.Boy.GetDescription();
            var str = CustomAttributeCommon<DescriptionAttribute>.GetCustomAttribute(typeof(GenderEnum),
                GenderEnum.Boy.ToString());
            var result = typeof(GenderEnum).ToEnumAndAttributes<ENameAttribute>();
            var result2 = typeof(User).ToEnumAndAttributes<ENameAttribute>();
        }
    }

    public enum GenderEnum
    {
        /// <summary>
        /// 男孩
        /// </summary>
        [EName("男")] [Description("男孩")] Boy = 1,

        /// <summary>
        /// 女孩
        /// </summary>
        [EName("女")] [Description("女孩")] Girl = 2
    }

    public class User
    {
        [EName("姓名")] public string Name { get; set; }

        [EName("性别")] public bool Gender { get; set; }
    }
}
