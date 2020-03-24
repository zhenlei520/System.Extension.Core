// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 多线程任务，可控制最大线程数（有响应值）
    /// </summary>
    /// <typeparam name="T">传入任务参数</typeparam>
    /// <typeparam name="T2">任务响应的信息</typeparam>
    public class TaskCommon<T, T2> : TaskBaseCommon
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息3000ms</param>
        public TaskCommon(int maxThread, int duration = 3000) : base(maxThread, duration)
        {
        }

        #region 添加任务(有返回值)

        /// <summary>
        /// 添加任务(有返回值)
        /// </summary>
        /// <param name="item">传入任务参数</param>
        /// <param name="func">需要执行的任务，返回值为任务完成后需要接受的值</param>
        /// <param name="taskFinishAction">任务结束后需要返回的结果，如果执行成功，其中T2的值为func委托的响应值</param>
        public void Add(T item, Func<T, CancellationTokenSource, T2> func,
            Action<bool, T2, Exception> taskFinishAction = null)
        {
            Guid guid = Guid.NewGuid();
            AwaitList.Add(guid, new TaskJobParam<T>(guid, item));
            StartProcessForWait(func, taskFinishAction);
        }

        /// <summary>
        /// 添加任务集合(有返回值)
        /// </summary>
        /// <param name="list">传入任务参数</param>
        /// <param name="func">需要执行的任务，返回值为任务完成后需要接受的值</param>
        /// <param name="taskFinishAction">任务结束后需要返回的结果，如果执行成功，其中T2的值为func委托的响应值</param>
        /// <typeparam name="T"></typeparam>
        public void AddRang(List<T> list, Func<T, CancellationTokenSource, T2> func,
            Action<bool, T2, Exception> taskFinishAction)
        {
            list.ForEach(item => { Add(item, func, taskFinishAction); });
        }

        #endregion

        #region private methods

        #region 开始新的任务

        /// <summary>
        /// 开始新的任务
        /// </summary>
        /// <param name="item"></param>
        /// <param name="func"></param>
        /// <param name="taskFinishAction"></param>
        private void StartNewProcess(TaskJobParam<T> item, Func<T, CancellationTokenSource, T2> func,
            Action<bool, T2, Exception> taskFinishAction)
        {
            if (!OnGoingList.ContainsKey(item.Id))
            {
                OnGoingList.Add(item.Id, item.Data);
            }
            else
            {
                StartProcessForWait(func, taskFinishAction);
            }

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = Task.Factory.StartNew(() =>
                    func.Invoke(item.Data, cts), token)
                .ContinueWith((res) =>
                {
                    try
                    {
                        if (res == null)
                        {
                            taskFinishAction?.Invoke(false, default(T2), null);
                        }
                        else if (res.Exception != null)
                        {
                            taskFinishAction?.Invoke(false, ObjectCommon.SafeObject(res.Result != null, () =>
                                    ValueTuple.Create(res.Result, default(T2))),
                                res.Exception);
                        }
                        else
                        {
                            taskFinishAction?.Invoke(true,
                                ObjectCommon.SafeObject(res.Result != null, () =>
                                    ValueTuple.Create(res.Result, default(T2))),
                                null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                    finally
                    {
                        if (OnGoingList.ContainsKey(item.Id))
                        {
                            OnGoingList.Remove(item.Id);
                        }

                        StartProcessForWait(func, taskFinishAction);
                    }
                }, token);
        }

        #endregion

        #region 开始执行等待进行的任务

        /// <summary>
        /// 开始执行等待进行的任务
        /// </summary>
        private void StartProcessForWait(Func<T, CancellationTokenSource, T2> func,
            Action<bool, T2, Exception> taskFinishAction)
        {
            if (AwaitList.Count > 0 && IsStartNewProcess)
            {
                TaskJobParam<T> taskJobParam = base.GetFirstJob<TaskJobParam<T>>(AwaitList);
                if (taskJobParam != null)
                {
                    if (AwaitList.ContainsKey(taskJobParam.Id))
                    {
                        AwaitList.Remove(taskJobParam.Id);
                    }

                    StartNewProcess(taskJobParam, func, taskFinishAction);
                }
            }
            else
            {
                if (_duration > 0)
                {
                    Thread.Sleep(_duration);
                }
            }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 多线程任务，可控制最大线程数（无响应值）
    /// </summary>
    /// <typeparam name="T">传入任务参数</typeparam>
    public class TaskCommon<T> : TaskBaseCommon
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息3000ms</param>
        public TaskCommon(int maxThread, int duration = 3000) : base(maxThread, duration)
        {
        }

        #region 添加任务(无响应值)

        /// <summary>
        /// 添加任务(无响应值)
        /// </summary>
        /// <param name="item">传入任务参数</param>
        /// <param name="action">需要执行的任务</param>
        public void Add(T item, Action<T, CancellationTokenSource> action)
        {
            Guid guid = Guid.NewGuid();
            AwaitList.Add(guid, new TaskJobParam<T>(guid, item));
            StartProcessForWait(action);
        }

        /// <summary>
        /// 添加任务集合(无响应值)
        /// </summary>
        /// <param name="list">传入任务参数</param>
        /// <param name="action">需要执行的任务</param>
        /// <typeparam name="T">任务参数类型</typeparam>
        public void AddRang(List<T> list, Action<T, CancellationTokenSource> action)
        {
            list.ForEach(item => { Add(item, action); });
        }

        #endregion

        #region private methods

        #region 开始新的任务

        /// <summary>
        /// 开始新的任务
        /// </summary>
        /// <param name="item"></param>
        /// <param name="action"></param>
        private void StartNewProcess(TaskJobParam<T> item, Action<T, CancellationTokenSource> action)
        {
            if (!OnGoingList.ContainsKey(item.Id))
            {
                OnGoingList.Add(item.Id, item.Data);
            }
            else
            {
                StartProcessForWait(action);
            }

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = Task.Factory.StartNew(() =>
                    action.Invoke(item.Data, cts), token)
                .ContinueWith((res) =>
                {
                    try
                    {
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                    finally
                    {
                        if (OnGoingList.ContainsKey(item.Id))
                        {
                            OnGoingList.Remove(item.Id);
                        }

                        StartProcessForWait(action);
                    }
                }, token);
        }

        #endregion

        #region 开始执行等待进行的任务

        /// <summary>
        /// 开始执行等待进行的任务
        /// </summary>
        private void StartProcessForWait(Action<T, CancellationTokenSource> action)
        {
            if (AwaitList.Count > 0 && IsStartNewProcess)
            {
                TaskJobParam<T> taskJobParam = base.GetFirstJob<TaskJobParam<T>>(AwaitList);
                if (taskJobParam != null)
                {
                    if (AwaitList.ContainsKey(taskJobParam.Id))
                    {
                        AwaitList.Remove(taskJobParam.Id);
                    }

                    StartNewProcess(taskJobParam, action);
                }
            }
            else
            {
                if (_duration > 0)
                {
                    Thread.Sleep(_duration);
                }
            }
        }

        #endregion

        #endregion
    }

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
        public static void WaitAll(params Action[] actions)
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
        /// 并发执行多个操作
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void ParallelExecute(params Action[] actions)
        {
            Parallel.Invoke(actions);
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
        /// <typeparam name="T"></typeparam>
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
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
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
