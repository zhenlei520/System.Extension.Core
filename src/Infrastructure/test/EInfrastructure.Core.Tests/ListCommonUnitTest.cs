// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    public class ListCommonUnitTest : BaseUnitTest
    {
        [Fact]
        public void Add()
        {
            List<Person> personList = new List<Person>
            {
                new Person
                {
                    Name = "小明"
                }
            };
            List<Person> personList2 = new List<Person>
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
            var persons = personList.Add(personList2, true);
            Assert.Equal(2, persons.Count);
        }

        [Fact]
        public void Minus()
        {
            List<Person> personList = new List<Person>
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
            List<Person> personList2 = new List<Person>
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
            var persons = personList.ExceptNew(personList2);
            persons = personList.Except(personList2);
        }

        [Fact]
        public void Remove()
        {
            List<Person> personList = new List<Person>
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
            personList.RemoveNew(personList.FirstOrDefault());
        }

        [Fact]
        public void RemoveRangeNew()
        {
            List<Person> personList = new List<Person>
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
            //personList.RemoveRangeNew(personList);
            personList.RemoveRangeNew(x => x.Name == "小明花2");
        }

        [Fact]
        public void ConvertListToString()
        {
            List<int> idList = new List<int>
            {
                1, 2, 3
            };
            string str = idList.ConvertListToString(',');

            List<string> nameList = new List<string>
            {
                "小李",
                " ",
                "小红"
            };
            str = nameList.ConvertListToString(',', true, true);
            List<string> nameList2 = null;
            str = nameList2.ConvertListToString(',', true, true);
        }

        [Fact]
        public void ListPager()
        {
            List<Person> personList = new List<Person>
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
            var pageData = personList.ListPager(1, 1, true);

            personList.ListPager(
                newpersonList =>
                {
                    newpersonList.ForEach(person =>
                    {
                        Console.WriteLine("一起上Github看源码吧");
                    });
                }, 1, 1);
        }

        [Fact]
        public void AddNew()
        {
            Person person = new Person()
            {
                Name = "小明",
                Tags = new List<string> { "帅哥" }
            };
            person.Tags = person.Tags.AddNew("聪明");
            Console.WriteLine(person.TagJson);
        }

        [Fact]
        public void AddNewMult()
        {
            Person person = new Person
            {
                Name = "小明",
                Tags = new List<string> { "帅哥" }
            };
            person.Tags = person.Tags.AddNewMult(new List<string>
            {
                "聪明",
                "伶俐"
            });
            person.Tags.RemoveNew(x => x == "");
            Console.WriteLine(person.TagJson);
        }

        [Serializable]
        public class Person
        {
            public string Name { get; set; }

            public List<string> Tags
            {
                get => (List<string>)new NewtonsoftJsonProvider().Deserialize(TagJson, typeof(List<string>));
                set => TagJson = new NewtonsoftJsonProvider().Serializer(value);
            }

            public string TagJson { get; private set; } = "[]";
        }
    }
}
