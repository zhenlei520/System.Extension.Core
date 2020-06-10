using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 线程任务
    /// </summary>
    public class TaskCommonUnitTest : BaseUnitTest
    {
        public TaskCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 多线程输出名字
        /// </summary>
        [Fact]
        public void Test()
        {
            TaskCommon<string, object> taskCommon = new TaskCommon<string, object>(20, 0);

            List<Users> userses = new List<Users>();
            for (var i = 0; i < 30; i++)
            {
                userses.Add(new Users()
                {
                    Name = "我的名字是" + i,
                    Gender = i % 2 == 1
                });
            }

            int success = 0;
            foreach (var item in userses)
            {
                if (item.Gender)
                {
                    taskCommon.Add(item.Name, (name, cts) =>
                    {
                        Console.WriteLine($"性别：男，我的名字是：" + name);
                        success++;
                        return "结束了" + "我的名字是：" + name;
                    });
                }
                else
                {
                    taskCommon.Add(item.Name, (name, cts) =>
                    {
                        Console.WriteLine($"性别：女，我的名字是：" + name);
                        success++;
                        return "结束了" + "我的名字是：" + name;
                    });
                }
            }

            taskCommon.Run();
            while (success < userses.Count)
            {
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 多线程输出名字
        /// </summary>
        [Fact]
        public void Test2()
        {
            TaskCommon<string> taskCommon = new TaskCommon<string>(20);

            List<Users> userses = new List<Users>();
            for (var i = 0; i < 30; i++)
            {
                userses.Add(new Users()
                {
                    Name = "我的名字是" + i
                });
            }

            foreach (var item in userses)
            {
                taskCommon.Add(item.Name, (name, cts) =>
                {
                    Console.WriteLine("我的名字是：" + name);
                    Thread.Sleep(new Random().Next(1000, 3999));
                });
            }
        }

        /// <summary>
        /// 多线程输出名字
        /// </summary>
        [Fact]
        public void Test3()
        {
            int index = 0;
            TaskPool<string> taskCommon = null;
            taskCommon = new TaskPool<string>(200, (name) =>
            {
                lock (index + "")
                {
                    index++;
                    Console.WriteLine("我的名字是：" + name + "，线程id：" + Task.CurrentId + "，任务index：" +
                                      index);
                    Thread.Sleep(new Random().Next(100, 300));
                }
            }, () =>
            {
                Console.WriteLine("线程已销毁");
            }, 300);

            List<Users> userses = new List<Users>();
            for (var i = 0; i < 500; i++)
            {
                userses.Add(new Users()
                {
                    Name = "我的名字是" + i
                });
            }

            foreach (var item in userses)
            {
                taskCommon.AddJob(item.Name);
            }

            taskCommon.SetContinueTimer(0);
            taskCommon.Run();
            while (index < userses.Count + 1)
            {
                // Thread.Sleep(10000);
                // Thread.Yield();
                Thread.Sleep(100);
            }
        }

        public class Users
        {
            public string Name { get; set; }

            public bool Gender { get; set; }
        }
    }
}
