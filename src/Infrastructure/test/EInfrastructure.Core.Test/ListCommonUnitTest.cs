// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Test
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
            var persons = personList.Add(personList2);
            persons = personList.Add(personList2, true);
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
            string str = idList.ConvertToString(',');

            List<string> nameList = new List<string>
            {
                "小李",
                " ",
                "小红"
            };
            str = nameList.ConvertToString(',', true, true);
            List<string> nameList2 = null;
            str = nameList2.ConvertToString(',', true, true);
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
                newpersonList => { newpersonList.ForEach(person => { Console.WriteLine("一起上Github看源码吧"); }); }, 1, 1);
        }

        [Fact]
        public void AddNew()
        {
            Person person = new Person()
            {
                Name = "小明",
                Tags = new List<string> {"帅哥"}
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
                Tags = new List<string> {"帅哥"}
            };
            person.Tags = person.Tags.AddNewMult(new List<string>
            {
                "聪明",
                "伶俐"
            });
            person.Tags.RemoveNew(x => x == "");
            Console.WriteLine(person.TagJson);
        }


        [Fact]
        public void Compare()
        {
            List<int> oldIdList = new List<int>()
            {
                1, 2
            };
            List<int> newIdList = new List<int>()
            {
                1, 3
            };
            var res = oldIdList.Compare(newIdList);
        }

        [Fact]
        public void Compare2()
        {
            List<Person> oldIdList = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Scare = 1,
                    Name = "张三"
                },
                new Person()
                {
                    Id = 1,
                    Scare = 2,
                    Name = "梨花"
                },
                new Person()
                {
                    Id = 2,
                    Name = "李四"
                }
            };
            List<Person> newIdList = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "张三2"
                },
                new Person()
                {
                    Id = 3,
                    Name = "王五"
                }
            };
            var list = oldIdList.OrderBy<Person>(new List<KeyValuePair<string, bool>>()
            {
                new KeyValuePair<string, bool>("Id", true),
                new KeyValuePair<string, bool>("Scare",true)
            }).ToList();
            var res = oldIdList.Compare<Person, int>(newIdList);
        }

        [Serializable]
        public class Person : IEntity<int>
        {
            public int Id { get; set; }
            public string Name { get; set; }

            /// <summary>
            /// 分数
            /// </summary>
            public int Scare { get; set; }

            public List<string> Tags
            {
                get => (List<string>) new NewtonsoftJsonProvider().Deserialize(TagJson, typeof(List<string>));
                set => TagJson = new NewtonsoftJsonProvider().Serializer(value);
            }

            public string TagJson { get; private set; }
        }
    }
}
