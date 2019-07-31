// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Test.Base;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    public class ListCommonUnitTest : BaseUnitTest
    {
        public ListCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Add()
        {
            List<Person> personList = new List<Person>()
            {
                new Person
                {
                    Name = "小明"
                }
            };
            List<Person> personList2 = new List<Person>()
            {
                new Person
                {
                    Name = "小明"
                },
                new Person
                {
                    Name = "小明花"
                }
            };
            var persons = personList.Add(personList2);
            persons = personList.Add(personList2, true);
        }

        [Fact]
        public void Minus()
        {
            List<Person> personList = new List<Person>()
            {
                new Person
                {
                    Name = "小明"
                },
                new Person
                {
                    Name = "小明花2"
                }
            };
            List<Person> personList2 = new List<Person>()
            {
                new Person
                {
                    Name = "小明"
                },
                new Person
                {
                    Name = "小明花"
                }
            };
            var persons = personList.Minus(personList2);
            persons = personList.Minus(personList2, true);
        }

        [Fact]
        public void ConvertListToString()
        {
            List<int> idList = new List<int>()
            {
                1, 2, 3
            };
            string str = idList.ConvertListToString(',');

            List<string> nameList = new List<string>()
            {
                "小李",
                "",
                "小红"
            };
            str = nameList.ConvertListToString(',', true, true);
        }

        [Serializable]
        public class Person
        {
            public string Name { get; set; }
        }
    }
}
