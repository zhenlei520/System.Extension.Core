// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 多线程任务（有响应值）
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
        public void Add(T item, Func<T, T2> func,
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
        public void AddRang(List<T> list, Func<T, T2> func,
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
        private void StartNewProcess(TaskJobParam<T> item, Func<T, T2> func,
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
                    func.Invoke(item.Data), token)
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
                            taskFinishAction?.Invoke(false,ObjectCommon.SafeObject(res.Result != null,()=>
                                    ValueTuple.Create(res.Result, default(T2))),
                                res.Exception);
                        }
                        else
                        {
                            taskFinishAction?.Invoke(true,
                                ObjectCommon.SafeObject(res.Result != null,()=>
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
        private void StartProcessForWait(Func<T, T2> func,
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
    /// 多线程任务（无响应值）
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

        #region 添加任务(有返回值)

        /// <summary>
        /// 添加任务(有返回值)
        /// </summary>
        /// <param name="item">传入任务参数</param>
        /// <param name="action">需要执行的任务</param>
        public void Add(T item, Action<T> action)
        {
            Guid guid = Guid.NewGuid();
            AwaitList.Add(guid, new TaskJobParam<T>(guid, item));
            StartProcessForWait(action);
        }

        /// <summary>
        /// 添加任务集合(有返回值)
        /// </summary>
        /// <param name="list">传入任务参数</param>
        /// <param name="action">需要执行的任务</param>
        /// <typeparam name="T">任务参数类型</typeparam>
        public void AddRang(List<T> list, Action<T> action)
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
        private void StartNewProcess(TaskJobParam<T> item, Action<T> action)
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
                    action.Invoke(item.Data), token)
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
        private void StartProcessForWait(Action<T> action)
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
}
