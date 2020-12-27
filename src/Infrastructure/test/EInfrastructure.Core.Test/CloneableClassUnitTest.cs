// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Infrastructure;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
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

        /// <summary>
        /// 新深拷贝
        /// </summary>
        [Fact]
        public void DeepCloneNew()
        {
            People people = new People()
            {
                Name = "张三",
                Age = 18,
                Courses = new List<Course>()
                {
                    new Course()
                    {
                        Name = "语文",
                        Scores = new List<Score>()
                        {
                            new Score()
                            {
                                Value = 90
                            },

                            new Score()
                            {
                                Value = 80
                            }
                        }
                    },
                    new Course()
                    {
                        Name = "数学",
                        Scores = new List<Score>()
                        {
                            new Score()
                            {
                                Value = 70
                            },

                            new Score()
                            {
                                Value = 60
                            }
                        }
                    }
                }
            };

            People people2 = new People()
            {
                Name = "张三2",
                Age = 182,
                Courses = new List<Course>()
                {
                    new Course()
                    {
                        Name = "语文2",
                        Scores = new List<Score>()
                        {
                            new Score()
                            {
                                Value = 902
                            },

                            new Score()
                            {
                                Value = 802
                            }
                        }
                    },
                    new Course()
                    {
                        Name = "数学2",
                        Scores = new List<Score>()
                        {
                            new Score()
                            {
                                Value = 702
                            },

                            new Score()
                            {
                                Value = 602
                            }
                        }
                    }
                }
            };

            var p=Copier<People>.Copy(people);

            p.Name = "张三Copy";
            p.Courses.ForEach(item =>
            {
                item.Name = item.Name +"Copy";
                item.Scores.ForEach(item2 =>
                {
                    item2.Value = item2.Value + 100;
                });
            });
            var people3 = people.DeepCopy<People, People>();

            var people4 = people2.DeepCopy<People, People>();
        }


        #region 测试

        /// <summary>
        /// 人物
        /// </summary>
        public class People
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 年龄
            /// </summary>
            public int Age { get; set; }

            /// <summary>
            /// 课程信息
            /// </summary>
            public List<Course> Courses { get; set; }
        }

        /// <summary>
        /// 课程
        /// </summary>
        public class Course
        {
            /// <summary>
            /// 课程名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 分数
            /// </summary>
            public List<Score> Scores { get; set; }
        }

        public class Score
        {
            /// <summary>
            /// 分数
            /// </summary>
            public int Value { get; set; }
        }

        #endregion

        /// <summary>
    /// 表达式树复制对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Copier<T>
    {
        private static readonly ParameterExpression ParameterExpression = Expression.Parameter(typeof(T), "p");
        private static Func<T, T> _func;
        private static readonly Dictionary<string, Expression> DictRule = new Dictionary<string, Expression>();

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Copy(T source)
        {
            if (_func != null)
            {
                return _func.Invoke(source);
            }

            var memberBindings = new List<MemberBinding>();
            foreach (var item in typeof(T).GetProperties())
            {
                if (DictRule.ContainsKey(item.Name))
                {
                    MemberBinding memberBinding = Expression.Bind(item, DictRule[item.Name]);
                    memberBindings.Add(memberBinding);
                }
                else
                {
                    var tInProperty = typeof(T).GetProperty(item.Name);
                    var tInField = typeof(T).GetField(item.Name);
                    if (tInProperty != null || tInField != null)
                    {
                        MemberExpression property = Expression.PropertyOrField(ParameterExpression, item.Name);
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindings.Add(memberBinding);
                    }
                }
            }

            foreach (var item in typeof(T).GetFields())
            {
                if (DictRule.ContainsKey(item.Name))
                {
                    MemberBinding memberBinding = Expression.Bind(item, DictRule[item.Name]);
                    memberBindings.Add(memberBinding);
                }
                else
                {
                    var tInProperty = typeof(T).GetProperty(item.Name);
                    var tInField = typeof(T).GetField(item.Name);
                    if (tInProperty != null || tInField != null)
                    {
                        MemberExpression property = Expression.PropertyOrField(ParameterExpression, item.Name);
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindings.Add(memberBinding);
                    }
                }
            }

            var memberInitExpression = Expression.MemberInit(Expression.New(typeof(T)), memberBindings.ToArray());
            var lambda = Expression.Lambda<Func<T, T>>(memberInitExpression, ParameterExpression);
            _func = lambda.Compile();
            return _func.Invoke(source);
        }
    }
    }

}
