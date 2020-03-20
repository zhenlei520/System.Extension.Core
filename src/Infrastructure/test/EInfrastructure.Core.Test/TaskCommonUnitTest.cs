using System;
using System.Collections.Generic;
using System.Threading;
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
            TaskCommon<string, object> taskCommon = new TaskCommon<string, object>(20);

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
                taskCommon.Add(item.Name, (name) =>
                {
                    Console.WriteLine("我的名字是：" + name);
                    Thread.Sleep(new Random().Next(1000, 3999));
                    return "结束了" + "我的名字是：" + name;
                }, (state, res, exception) =>
                {
                    if (state)
                    {
                    }
                });
            }
        }

        public class Users
        {
            public string Name { get; set; }
        }
    }
}
