// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Tools.Common.Tasks.Request;

namespace EInfrastructure.Core.Tools.Common.Tasks
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
        private readonly Queue<TaskJobRequest<T>> _awaitList;

        /// <summary>
        /// 进行中的任务
        /// </summary>
        private readonly HashSet<TaskJobRequest<T>> _onGoingList;

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
            this._awaitList = new Queue<TaskJobRequest<T>>();
            this._onGoingList = new HashSet<TaskJobRequest<T>>();
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
        internal bool IsFinish
        {
            get
            {
                lock (_awaitList)
                {
                    return _awaitList.Count == 0;
                }
            }
        }

        #endregion

        #region 添加任务

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="taskJobBaseParamArray"></param>
        internal void AddJob(TaskJobRequest<T>[] taskJobBaseParamArray)
        {
            foreach (var taskJobRequest in taskJobBaseParamArray)
            {
                lock (_awaitList)
                {
                    if (this._awaitList.All(x => x.Id != taskJobRequest.Id))
                    {
                        this._awaitList.Enqueue(taskJobRequest);
                    }
                }
            }
        }

        #endregion

        #region 移除任务

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskJobBaseParam"></param>
        internal void RemoveJob(TaskJobRequest<T> taskJobBaseParam)
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
        internal bool GetNextJob(out TaskJobRequest<T> jobBaseParam)
        {
            lock (this._onGoingList)
            {
                lock (this._awaitList)
                {
                    try
                    {
                        if (this._awaitList.Count > 0)
                        {
                            jobBaseParam = this._awaitList.Dequeue();
                            if (jobBaseParam != null)
                            {
                                this._onGoingList.Add(jobBaseParam);
                            }

                            return jobBaseParam != null;
                        }
                        else
                        {
                            jobBaseParam = null;
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        jobBaseParam = null;
                        return false;
                    }
                }
            }
        }

        #endregion

        #region 清空任务

        /// <summary>
        /// 清空任务
        /// </summary>
        internal void Clear()
        {
            lock (this._awaitList)
            {
                this._awaitList.Clear();
                this._onGoingList.Clear();
            }
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
}
