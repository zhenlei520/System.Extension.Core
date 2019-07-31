// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enum;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Test.Base;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    public class CloneableClassUnitTest : BaseUnitTest
    {
        public CloneableClassUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void DeepClone()
        {
            Person person = new Person
            {
                Name = "小明",
                Gender = GenderEnum.Boy
            };
            var newPerson = person.DeepClone(person);
            newPerson.Name = "小明哥";
            var newPerson2 = person.ShallowClone<Person>();
            Check.True(person.Name != newPerson.Name, "方法有误");
            Check.True(person.Name == newPerson2.Name, "方法有误");
        }

        /// <summary>
        /// 性别
        /// </summary>
        [Serializable]
        public class Person : CloneableClass
        {
            public string Name { get; set; }

            public GenderEnum Gender { get; set; }
        }
    }
}
