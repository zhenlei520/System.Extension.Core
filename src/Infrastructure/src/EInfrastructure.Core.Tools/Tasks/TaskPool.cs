// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Tools.Tasks.Request;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 线程池
    /// </summary>
    public class TaskPool<T>
    {
        private readonly TaskBaseCommon<T> _taskBaseCommon;

        /// <summary>
        ///
        /// </summary>
        private List<TaskBaseInfo> _tasks;

        /// <summary>
        /// 是否执行过已完成任务
        /// </summary>
        private bool _isExcuteFinish;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        private TaskPool(int maxThread, int duration = 0)
        {
            this._taskBaseCommon = new TaskBaseCommon<T>(maxThread, duration);
            this._tasks = new List<TaskBaseInfo>();
            this._isExcuteFinish = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="jobAction"></param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<T> jobAction, int duration = 0) : this(maxThread, duration)
        {
            this._jobAction = jobAction;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="jobAction">任务</param>
        /// <param name="finishTaskAction">任务执行完成后执行</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<T> jobAction, Action finishTaskAction,
            int duration = 0) : this(
            maxThread,
            jobAction,
            duration)
        {
            this._finishTaskAction = finishTaskAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="jobAction">任务</param>
        /// <param name="finishTaskAction">任务执行完成后执行</param>
        /// <param name="destroyJobAction">全部线程销毁后执行</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<T> jobAction, Action finishTaskAction, Action destroyJobAction,
            int duration = 0) : this(
            maxThread,
            jobAction,
            finishTaskAction,
            duration)
        {
            this._destroyTaskAction = destroyJobAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="jobAction">任务</param>
        /// <param name="errJobAction">错误委托任务</param>
        /// <param name="destroyJobAction">全部线程销毁后执行</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<T> jobAction, Action<T, Exception> errJobAction, Action destroyJobAction,
            int duration = 0) : this(
            maxThread,
            jobAction,
            destroyJobAction,
            duration)
        {
            this._errJobAction = errJobAction;
        }

        /// <summary>
        /// 持续多久无任务，线程关闭
        /// 当此时间大于0时，子线程需要连续达到此时间后才会触发
        /// </summary>
        private int _continueTimer { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        private bool IsRun;

        /// <summary>
        /// 工作任务
        /// </summary>
        private Action<T> _jobAction;

        /// <summary>
        /// 错误joib工作任务
        /// </summary>
        private Action<T, Exception> _errJobAction;

        /// <summary>
        /// 全部任务执行完成后回调
        /// </summary>
        private Action _finishTaskAction;

        /// <summary>
        /// 全部子线程销毁后完成后执行(任务执行完成)
        /// 当_continueTimer时间大于0时，可能会导致销毁时间拉长
        /// </summary>
        private Action _destroyTaskAction;

        #region 添加任务

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="item"></param>
        private void AddJob(T item)
        {
            this._isExcuteFinish = false;
            Guid guid = Guid.NewGuid();
            _taskBaseCommon.AddJob(new TaskJobRequest<T>(guid, item));
            if (this.IsRun)
            {
                CheckAndAddTask();
            }
        }

        /// <summary>
        /// 添加任务集合
        /// </summary>
        /// <param name="items"></param>
        public void AddJob(params T[] items)
        {
            items.ToList().ForEach(AddJob);
        }

        #endregion

        #region 启动任务

        /// <summary>
        /// 启动任务
        /// </summary>
        public void Run()
        {
            if (!IsRun)
            {
                IsRun = true;
                CheckAndAddTask();
                this._tasks.ForEach(task =>
                {
                    task.Run();
                    task.Task.Start();
                });
            }
        }

        #endregion

        #region 设置线程超时空闲被移除

        /// <summary>
        /// 设置线程超时空闲被移除
        /// 设置为-1时，子线程永不过期，永不释放
        /// </summary>
        /// <param name="continueTimer">空闲时长</param>
        public void SetContinueTimer(int continueTimer)
        {
            Check.True(continueTimer >= 0 || continueTimer == -1, "线程超时空闲时间有误");
            this._continueTimer = continueTimer;
        }

        #endregion

        #region 设置错误任务

        /// <summary>
        /// 设置错误任务
        /// </summary>
        /// <param name="errJobAction"></param>
        public void SetErrorJobAction(Action<T, Exception> errJobAction)
        {
            this._errJobAction = errJobAction;
        }

        #endregion

        #region 设置全部子线程完成后执行

        /// <summary>
        /// 设置全部子线程完成后执行
        /// </summary>
        /// <param name="finishTaskAction"></param>
        public void SetFinishJobAction(Action finishTaskAction)
        {
            this._finishTaskAction = finishTaskAction;
        }

        #endregion

        #region 设置全部子线程销毁后执行任务

        /// <summary>
        /// 设置全部子线程销毁后执行任务
        /// </summary>
        /// <param name="destroyTaskAction"></param>
        public void SetDestroyJobAction(Action destroyTaskAction)
        {
            this._destroyTaskAction = destroyTaskAction;
        }

        #endregion

        #region 停止任务

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            if (IsRun)
            {
                IsRun = false;
            }
        }

        #endregion

        #region 清空任务并停止

        /// <summary>
        /// 清空任务并停止
        /// </summary>
        public void Clear()
        {
            this._taskBaseCommon.Clear();
            this.IsRun = false;
        }

        #endregion

        #region private methods

        #region 检查任务数量

        /// <summary>
        /// 检查任务数量
        /// </summary>
        private void CheckAndAddTask()
        {
            while (this._tasks.Count < this._taskBaseCommon.GetMaxThread)
            {
                this._tasks.Add(new TaskBaseInfo(new Task(StartJob)));
            }
        }

        #endregion

        #region 执行任务

        /// <summary>
        /// 执行任务
        /// </summary>
        private void StartJob()
        {
            if (this._taskBaseCommon.GetNextJob(out TaskJobRequest<T> jobParam) && this.IsRun)
            {
                try
                {
                    _jobAction.Invoke(jobParam.Data);
                }
                catch (Exception ex)
                {
                    this._errJobAction?.Invoke(jobParam.Data, ex);
                }

                this._taskBaseCommon.RemoveJob(jobParam);
            }
            else
            {
                Thread.Sleep(this._taskBaseCommon._duration);
            }

            var taskInfo = GetTaskInfo();
            if (jobParam == null)
            {
                taskInfo.Stop();
                if (CheckIsDestroy(taskInfo))
                {
                    lock (this._tasks)
                    {
                        this._tasks.Remove(taskInfo);
                        if (this._tasks.All(x => x.IsFree) && !this._isExcuteFinish)
                        {
                            this._isExcuteFinish = true;
                            this._finishTaskAction?.Invoke();
                        }

                        if (this._tasks.Count == 0)
                        {
                            this._destroyTaskAction?.Invoke();
                        }
                    }
                }
                else
                {
                    StartJob();
                }
            }
            else
            {
                RunCurrentTask(taskInfo);
                StartJob();
            }
        }

        #region 得到当前线程任务

        /// <summary>
        /// 得到当前线程任务
        /// </summary>
        /// <returns></returns>
        private TaskBaseInfo GetTaskInfo()
        {
            lock (this._tasks)
            {
                return this._tasks.FirstOrDefault(x => x.Task.Id == Task.CurrentId);
            }
        }

        #endregion

        #region 重置当前线程任务

        /// <summary>
        /// 重置当前线程任务
        /// </summary>
        private void RunCurrentTask(TaskBaseInfo taskBaseInfo)
        {
            if (!(this._continueTimer != -1 && taskBaseInfo.GetStagnationTotalMilliseconds() > this._continueTimer))
            {
                taskBaseInfo.Reset();
            }
        }

        #endregion

        #region 检查线程是否可以被销毁

        /// <summary>
        /// 检查线程是否可以被销毁
        /// </summary>
        /// <param name="taskBaseInfo"></param>
        /// <returns></returns>
        private bool CheckIsDestroy(TaskBaseInfo taskBaseInfo)
        {
            return this._continueTimer != -1 && taskBaseInfo.GetStagnationTotalMilliseconds() > this._continueTimer;
        }

        #endregion

        #endregion

        #endregion
    }

    /// <summary>
    /// 线程基本信息
    /// </summary>
    internal class TaskBaseInfo
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        public TaskBaseInfo(Task task)
        {
            this.Task = task;
            this.IsFree = true;
            this.StagnationTime = Stopwatch.StartNew();
        }

        /// <summary>
        /// 任务信息
        /// </summary>
        ///
        internal Task Task { get; private set; }

        /// <summary>
        /// 线程是否空间
        /// </summary>
        public bool IsFree { get; private set; }

        /// <summary>
        /// 停滞时间
        /// </summary>
        public Stopwatch StagnationTime { get; private set; }

        /// <summary>
        /// 线程启动
        /// </summary>
        public void Run()
        {
            this.IsFree = false;
            this.StagnationTime = null;
        }

        /// <summary>
        /// 线程停滞
        /// </summary>
        public void Stop()
        {
            if (!this.IsFree)
            {
                this.IsFree = true;

                this.StagnationTime = Stopwatch.StartNew();
            }
        }

        /// <summary>
        /// 重置线程
        /// </summary>
        public void Reset()
        {
            this.IsFree = false;
            this.StagnationTime = null;
        }

        /// <summary>
        /// 得到线程停滞时间（ms）
        /// 返回-1为线程未停滞
        /// </summary>
        /// <returns></returns>
        public long GetStagnationTotalMilliseconds()
        {
            try
            {
                if (!this.IsFree)
                {
                    return -1;
                }

                this.StagnationTime?.Stop();
                var timer = this.StagnationTime?.ElapsedMilliseconds ?? 0;
                this.StagnationTime?.Start();
                return timer;
            }
            catch (Exception e)
            {
                Console.WriteLine("异常：" + e.ExtractAllStackTrace());
                return 0;
            }
        }
    }
}
