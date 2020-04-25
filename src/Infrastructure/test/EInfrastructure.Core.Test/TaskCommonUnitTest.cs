using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Tasks;
using EInfrastructure.Core.Tools.Tasks.Dto;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 线程任务
    /// </summary>
    public class TaskCommonUnitTest : BaseUnitTest
    {

        public TaskCommonUnitTest() : base()
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
                taskCommon.Add(item.Name, (name, cts) =>
                {
                    cts.Cancel(); //取消任务
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
                    output.WriteLine("我的名字是：" + name);
                    Thread.Sleep(new Random().Next(1000, 3999));
                });
            }
        }

        /// <summary>
        /// 多线程并行计算获取学生的成绩情况
        /// 与原来的并行任务相比，最大的特点是此方法有响应值
        /// </summary>
        [Fact]
        public void ParallelExecute()
        {
            DateTime startTime = DateTime.Now;
            List<string> studentList = "小李,小王,小红".ConvertStrToList<string>(',');
            List<JobItem> jobItems = studentList.Select(x => new JobItem(x)).ToList();
            var list = TaskCommon.ParallelExecute((item) =>
            {
                if (item.Source.ToString() == "小王")
                {
                    item.SetResponse("优秀");
                    Thread.Sleep(3000);
                }
                else if (item.Source.ToString() == "小李")
                {
                    item.SetResponse("良好");
                    Thread.Sleep(1000);
                }
                else if (item.Source.ToString() == "小红")
                {
                    item.SetResponse("学渣");
                    Thread.Sleep(2000);
                }

                //线程等待是为了估计造成计算缓慢的情况
            }, jobItems);
            double duration = (DateTime.Now - startTime).TotalMilliseconds;
            output.WriteLine($"结果是：{new NewtonsoftJsonProvider().Serializer(list)}" + "，消耗时间：{duration}s");
        }

        public class Users
        {
            public string Name { get; set; }
        }
    }
}
