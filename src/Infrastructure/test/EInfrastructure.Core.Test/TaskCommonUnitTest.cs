using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                Console.WriteLine("任务执行完成");
            }, () =>
            {
                Console.WriteLine("线程已销毁");
            }, 300);

            List<Users> userses = new List<Users>();
            for (var i = 0; i < 9999; i++)
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
            while (index < userses.Count)
            {
                // Thread.Sleep(10000);
                // Thread.Yield();
                Thread.Sleep(100);
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
        }

        public class Users
        {
            public string Name { get; set; }

            public bool Gender { get; set; }
        }
    }
}
