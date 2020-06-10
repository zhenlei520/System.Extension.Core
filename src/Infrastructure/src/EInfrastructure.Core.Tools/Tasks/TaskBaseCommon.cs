// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 基础类
    /// </summary>
    internal class TaskBaseCommon<T>
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        private int _maxThread;

        /// <summary>
        /// 得到最大线程数
        /// </summary>
        public int GetMaxThread => _maxThread;

        /// <summary>
        /// 等待中的任务
        /// </summary>
        private readonly ConcurrentQueue<TaskJobBaseParam<T>> _awaitList;

        /// <summary>
        /// 进行中的任务
        /// </summary>
        private readonly HashSet<TaskJobBaseParam<T>> _onGoingList;

        /// <summary>
        /// 无任务后休息的时间ms
        /// </summary>
        internal readonly int _duration;

        /// <summary>
        /// 结束后执行
        /// </summary>
        internal readonly Action FinishAction;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息500ms(防止死循环)</param>
        internal TaskBaseCommon(int maxThread, int duration = 500)
        {
            this._awaitList = new ConcurrentQueue<TaskJobBaseParam<T>>();
            this._onGoingList = new HashSet<TaskJobBaseParam<T>>();
            _maxThread = maxThread;
            _duration = duration;
            Check.True(_duration >= 0, "duration设置有误");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="finishAction">执行成功</param>
        /// <param name="duration">默认无任务后休息500ms(防止死循环)</param>
        internal TaskBaseCommon(int maxThread, Action finishAction, int duration = 500) : this(maxThread, duration)
        {
            if (finishAction != null)
            {
                this.FinishAction = finishAction;
            }
        }

        #region methods

        #region 判断是否可开启新的任务

        /// <summary>
        /// 判断是否可开启新的任务
        /// </summary>
        internal bool IsCanProcess => _onGoingList.Count < GetMaxThread;

        #endregion

        #region 判断线程是否全部结束

        /// <summary>
        /// 是否结束
        /// </summary>
        internal bool IsFinish => _awaitList.Count == 0;

        #endregion

        #region 添加任务

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="taskJobBaseParam"></param>
        internal void AddJob(TaskJobBaseParam<T> taskJobBaseParam)
        {
            if (this._awaitList.All(x => x.Id != taskJobBaseParam.Id))
            {
                this._awaitList.Enqueue(taskJobBaseParam);
            }
        }

        #endregion

        #region 移除任务

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskJobBaseParam"></param>
        internal void RemoveJob(TaskJobBaseParam<T> taskJobBaseParam)
        {
            lock (this._onGoingList)
            {
                if (this._onGoingList.Any(x => x.Id == taskJobBaseParam.Id))
                {
                    this._onGoingList.RemoveWhere(x => x.Id == taskJobBaseParam.Id);
                }
            }
        }

        #endregion

        #region 得到下一个任务

        /// <summary>
        /// 得到下一个任务
        /// </summary>
        /// <returns></returns>
        internal bool GetNextJob(out TaskJobBaseParam<T> jobBaseParam)
        {
            lock (this._onGoingList)
            {
                bool result = this._awaitList.TryDequeue(out jobBaseParam);
                if (result)
                {
                    this._onGoingList.Add(jobBaseParam);
                }

                return result;
            }
        }

        #endregion

        #region 清空任务

        /// <summary>
        /// 清空任务
        /// </summary>
        internal void Clear()
        {
            while (this._awaitList.TryDequeue(out _))
            {
                //清空等待中的任务
            }

            this._onGoingList.Clear();
        }

        #endregion

        #region 得到正在进行中的任务数量

        /// <summary>
        /// 得到正在进行中的任务数量
        /// </summary>
        /// <returns></returns>
        internal int GetOngoingCount()
        {
            lock (this._onGoingList)
            {
                return this._onGoingList.Count;
            }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 基础类
    /// </summary>
    internal class TaskBase2Common
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        private int _maxThread;

        /// <summary>
        /// 得到最大线程数
        /// </summary>
        public int GetMaxThread => _maxThread;

        /// <summary>
        /// 等待中的任务
        /// </summary>
        public readonly Hashtable AwaitList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 进行中的任务
        /// </summary>
        public readonly Hashtable OnGoingList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 执行任务完成数
        /// </summary>
        public int SuccessCount { get; private set; }

        /// <summary>
        /// 无任务后休息的时间
        /// </summary>
        public readonly int _duration;

        /// <summary>
        /// 结束后执行
        /// </summary>
        public readonly Action FinishAction;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息3000ms</param>
        public TaskBase2Common(int maxThread, int duration = 0)
        {
            _maxThread = maxThread;
            _duration = duration;
            SuccessCount = 0;
            Check.True(_duration >= 0, "duration设置有误");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="finishAction">执行成功</param>
        /// <param name="duration">默认无任务后休息3000ms</param>
        public TaskBase2Common(int maxThread, Action finishAction, int duration = 0) : this(maxThread, duration)
        {
            if (finishAction != null)
            {
                this.FinishAction = finishAction;
            }
        }

        #region methods

        #region 运行成功

        /// <summary>
        /// 运行成功
        /// </summary>
        public void RunSuccess()
        {
            this.SuccessCount++;
        }

        #endregion

        #endregion

        #region private methods

        #region 判断是否可开启新的任务

        /// <summary>
        /// 判断是否可开启新的任务
        /// </summary>
        public bool IsCanProcess => OnGoingList.Count < GetMaxThread;

        #endregion

        #region 判断线程是否全部结束

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool IsFinish => AwaitList.Count == 0;

        #endregion

        #region 得到第一个任务

        /// <summary>
        /// 得到第一个任务
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        internal T GetFirstJob<T>(Hashtable hashtable)
        {
            if (hashtable.Count == 0)
            {
                return default(T);
            }

            ArrayList arrayList = new ArrayList(hashtable.Keys);
            arrayList.Sort();
            return (T) hashtable[arrayList[0]];
        }

        #endregion

        #region 得到下一个待执行的任务并移除此任务

        /// <summary>
        /// 得到下一个待执行的任务并移除此任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal TaskJobBaseParam<T> GetNextJob2<T>()
        {
            lock (AwaitList)
            {
                var taskJobParam = GetFirstJob<TaskJobBaseParam<T>>(AwaitList);
                if (taskJobParam != null)
                {
                    if (AwaitList.ContainsKey(taskJobParam.Id))
                    {
                        AwaitList.Remove(taskJobParam.Id);
                    }
                }

                return taskJobParam;
            }
        }

        /// <summary>
        /// 得到下一个待执行的任务并移除此任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal TaskJobParam<T> GetNextJob<T>()
        {
            lock (AwaitList)
            {
                var taskJobParam = GetFirstJob<TaskJobParam<T>>(AwaitList);
                if (taskJobParam != null)
                {
                    if (AwaitList.ContainsKey(taskJobParam.Id))
                    {
                        AwaitList.Remove(taskJobParam.Id);
                    }
                }

                return taskJobParam;
            }
        }

        /// <summary>
        /// 得到下一个待执行的任务并移除此任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal TaskJobParam2<T, T2> GetNextJob<T, T2>()
        {
            lock (AwaitList)
            {
                var taskJobParam = GetFirstJob<TaskJobParam2<T, T2>>(AwaitList);
                if (taskJobParam != null)
                {
                    if (AwaitList.ContainsKey(taskJobParam.Id))
                    {
                        AwaitList.Remove(taskJobParam.Id);
                    }
                }

                return taskJobParam;
            }
        }

        #endregion

        #endregion
    }
}
