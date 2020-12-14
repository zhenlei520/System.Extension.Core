// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Tools.Common.Tasks.Dto;

namespace EInfrastructure.Core.Tools.Common.Tasks
{
    /// <summary>
    /// 多线程任务 无法控制最大线程数
    /// </summary>
    public static class TaskCommon
    {
        #region 执行多个操作，等待所有操作完成

        /// <summary>
        /// 执行多个操作，等待所有操作完成
        /// </summary>
        /// <param name="actions">操作集合</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("suggest use ParallelExecute")]
        public static void WaitAll(params Action[] actions)
        {
            WaitAll(TaskCreationOptions.None, actions);
        }

        /// <summary>
        /// 执行多个操作，等待所有操作完成
        /// </summary>
        /// <param name="taskCreationOptions">父子任务运行、长时间运行配置</param>
        /// <param name="actions">操作集合</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WaitAll(TaskCreationOptions taskCreationOptions, params Action[] actions)
        {
            if (actions == null)
                return;
            List<Task> tasks = new List<Task>();
            foreach (var action in actions)
                tasks.Add(Task.Factory.StartNew(action, TaskCreationOptions.None));
            Task.WaitAll(tasks.ToArray());
        }

        #endregion

        #region 并发执行多个操作

        /// <summary>
        /// 并发执行多个操作（无返回值）
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void ParallelExecute(params Action[] actions)
        {
            Parallel.Invoke(actions);
        }

        /// <summary>
        /// 并发执行多个操作（有返回值）
        /// </summary>
        /// <param name="action"></param>
        /// <param name="jobs">待执行的参数集合</param>
        /// <returns></returns>
        public static IEnumerable<JobItem> ParallelExecute(Action<JobItem> action, IEnumerable<JobItem> jobs)
        {
            IEnumerable<JobItem2> jobItemList = jobs.Select(x => new JobItem2(action, x.Source));
            var job2ResponseList = ParallelExecute(jobItemList);
            return job2ResponseList.Select(x => new JobItem(x.Source, x.Data));
        }

        /// <summary>
        /// 并发执行多个操作（有返回值）
        /// </summary>
        /// <param name="action">委托方法</param>
        /// <param name="jobs">待执行的参数集合</param>
        /// <returns></returns>
        public static IEnumerable<JobItem> ParallelExecute(Action<JobItem> action, params JobItem[] jobs)
        {
            return ParallelExecute(action, jobs.ToList());
        }

        /// <summary>
        /// 并发执行多个操作（有返回值）
        /// </summary>
        /// <param name="jobItems"></param>
        /// <returns></returns>
        public static IEnumerable<JobItem> ParallelExecute(params JobItem2[] jobItems)
        {
            return ParallelExecute(jobItems.ToList());
        }

        /// <summary>
        /// 并发执行多个操作（有返回值）
        /// </summary>
        /// <param name="jobItems"></param>
        /// <returns></returns>
        public static IEnumerable<JobItem2> ParallelExecute(IEnumerable<JobItem2> jobItems)
        {
            var watis = new List<EventWaitHandle>();
            foreach (var job in jobItems)
            {
                //创建句柄   true终止状态
                var handler = new ManualResetEvent(false);
                watis.Add(handler);
                //创建线程，传入线程参数
                Thread t = new Thread(ParallelExecuteResult);
                //启动线程
                t.Start(new Request.JobItemRequest(job, job.Action, handler));
            }

            WaitHandle.WaitAll(watis.ToArray());
            return jobItems;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        private static void ParallelExecuteResult(object data)
        {
            Request.JobItemRequest jobItem =
                (Request.JobItemRequest) data;
            if (jobItem == null)
            {
                return;
            }

            jobItem.Action.Invoke(jobItem.Job);
            jobItem.EventWaitHandle.Set();
        }

        #endregion

        #region 重复的并发执行操作

        /// <summary>
        /// 重复的并发执行操作
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="count">执行次数</param>
        /// <param name="options">并发执行配置</param>
        public static void ParallelExecute(Action action, int count = 1, ParallelOptions options = null)
        {
            if (options == null)
            {
                Parallel.For(0, count, i => action());
                return;
            }

            Parallel.For(0, count, options, i => action());
        }

        #endregion

        #region 创建线程任务

        #region 创建线程任务（有响应值）

        /// <summary>
        /// 创建线程任务（有响应值）
        /// </summary>
        /// <param name="func">线程委托，输入参数为可通过Cancel方法取消任务，输出参数为最后的结果</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> Create<T>(Func<CancellationTokenSource, T> func)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            return Task.Factory.StartNew(() => func.Invoke(cts), token);
        }

        /// <summary>
        /// 创建线程任务（有响应值）
        /// </summary>
        /// <param name="func">线程委托，输入参数为可通过Cancel方法取消任务，输出参数为最后的结果</param>
        /// <returns></returns>
        public static Task<object> Create(Func<CancellationTokenSource, object> func)
        {
            return Create<object>(func);
        }

        #endregion

        #region 创建线程任务（无响应值）

        /// <summary>
        /// 创建线程任务（无响应值）
        /// </summary>
        /// <param name="action">线程委托，输入参数为可通过Cancel方法取消任务</param>
        /// <returns></returns>
        public static Task Create(Action<CancellationTokenSource> action)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            return Task.Factory.StartNew(() => { action.Invoke(cts); }, token);
        }

        #endregion

        #endregion

        #region 单任务串行

        #region 单任务串行（有响应值）

        /// <summary>
        /// 单任务串行（有响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="func">委托任务</param>
        /// <typeparam name="T">第一个任务的返回类型</typeparam>
        /// <typeparam name="T2">方法返回类型</typeparam>
        /// <returns></returns>
        public static Task<T2> And<T, T2>(this Task<T> task, Func<T, T2> func)
        {
            return task.ContinueWith(res => func.Invoke(res.Result));
        }

        /// <summary>
        /// 单任务串行（有响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="func">委托任务</param>
        /// <typeparam name="T">第一个任务的返回类型</typeparam>
        /// <typeparam name="T2">方法返回类型</typeparam>
        /// <returns></returns>
        public static Task<T2> And<T, T2>(this Task<T> task, Func<T, CancellationTokenSource, T2> func)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            return task.ContinueWith(res => func.Invoke(res.Result, cts), token);
        }

        #endregion

        #region 单任务串行（无响应值）

        /// <summary>
        /// 单任务串行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action">委托任务</param>
        /// <returns></returns>
        public static Task And<T>(this Task<T> task, Action<T> action)
        {
            return task.ContinueWith(res => { action.Invoke(res.Result); });
        }

        /// <summary>
        /// 单任务串行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action">委托任务</param>
        /// <returns></returns>
        public static Task And<T>(this Task<T> task, Action<T, CancellationTokenSource> action)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            return task.ContinueWith(res => { action.Invoke(res.Result, cts); }, token);
        }

        /// <summary>
        /// 单任务串行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action">委托任务</param>
        /// <returns></returns>
        public static Task And(this Task task, Action action)
        {
            return task.ContinueWith(res => { action.Invoke(); });
        }

        /// <summary>
        /// 单任务串行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action">委托任务</param>
        /// <returns></returns>
        public static Task And(this Task task, Action<CancellationTokenSource> action)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            return task.ContinueWith(res => { action.Invoke(cts); }, token);
        }

        #endregion

        #endregion

        #region 多任务并行

        #region 多任务并行（无响应值）

        /// <summary>
        /// 多任务并行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Task Or(this Task task, params Action[] actions)
        {
            return task.ContinueWith(res => { ParallelExecute(actions); });
        }

        /// <summary>
        /// 多任务并行（无响应值）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Task Or(this Task task, params Action<CancellationTokenSource>[] actions)
        {
            return task.ContinueWith(res =>
            {
                List<Task> tasks = new List<Task>();
                foreach (var action in actions)
                {
                    var cts = new CancellationTokenSource();
                    var token = cts.Token;
                    tasks.Add(Task.Factory.StartNew(() => { action.Invoke(cts); }, token));
                }

                Task.WaitAll(tasks.ToArray());
            });
        }

        #endregion

        #endregion
    }
}
