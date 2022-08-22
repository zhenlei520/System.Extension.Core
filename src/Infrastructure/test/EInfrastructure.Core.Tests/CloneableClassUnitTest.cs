// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Infrastructure;
using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    public class CloneableClassUnitTest : BaseUnitTest
    {
        [Fact]
        public void DeepClone()
        {
            var item = new AssemblyProvider().GetAssemblies();
            Person person = new Person
            {
                Name = "小明",
                Gender = Gender.Boy
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

            public Gender Gender { get; set; }
        }
    }
}
