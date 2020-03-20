// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 多线程任务
    /// </summary>
    public class TaskCommon<T, T2>
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        private int _maxThread;

        /// <summary>
        /// 等待中的任务
        /// </summary>
        private readonly Hashtable AwaitList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 进行中的任务
        /// </summary>
        private readonly Hashtable OnGoingList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        public TaskCommon(int maxThread)
        {
            _maxThread = maxThread;
        }

        /// <summary>
        /// 得到最大线程数
        /// </summary>
        public int GetMaxThread => _maxThread;

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="item"></param>
        /// <param name="func"></param>
        /// <param name="taskFinishAction"></param>
        public void Add(T item, Func<T, T2> func,
            Action<bool, T2, Exception> taskFinishAction)
        {
            Guid guid = new Guid();
            AwaitList.Add(guid, new TaskJobParam<T>(guid, item));
            StartProcessForWait(func, taskFinishAction);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="taskFinishAction"></param>
        /// <typeparam name="T"></typeparam>
        public void AddRang(List<T> list, Func<T, T2> func,
            Action<bool, T2, Exception> taskFinishAction)
        {
            list.ForEach(item => { Add(item, func, taskFinishAction); });
        }

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
                            taskFinishAction?.Invoke(false, res.Result ?? default(T2),
                                res.Exception);
                        }
                        else
                        {
                            taskFinishAction?.Invoke(true, res.Result ?? default(T2), null);
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
            task.Start();
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
                TaskJobParam<T> taskJobParam = GetFirstJob(AwaitList);
                if (taskJobParam != null)
                {
                    StartNewProcess(taskJobParam, func, taskFinishAction);
                }
            }
        }

        #endregion

        #region private methods

        #region 判断是否可开启新的任务

        /// <summary>
        /// 判断是否可开启新的任务
        /// </summary>
        private bool IsStartNewProcess => OnGoingList.Count < _maxThread;

        #endregion

        #region 得到第一个任务

        /// <summary>
        /// 得到第一个任务
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        private TaskJobParam<T> GetFirstJob(Hashtable hashtable)
        {
            if (hashtable.Count == 0)
            {
                return null;
            }

            ArrayList arrayList = new ArrayList(hashtable.Keys);
            arrayList.Sort();
            return new TaskJobParam<T>((Guid) arrayList[0], (T) hashtable[arrayList[0]]);
        }

        #endregion

        #endregion
    }
}
