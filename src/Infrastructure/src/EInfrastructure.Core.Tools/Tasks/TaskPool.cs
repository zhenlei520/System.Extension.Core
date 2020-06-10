// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private List<KeyValuePair<Task, Stopwatch>> _tasks;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        private TaskPool(int maxThread, int duration = 0)
        {
            this._taskBaseCommon = new TaskBaseCommon<T>(maxThread, duration);
            this._tasks = new List<KeyValuePair<Task, Stopwatch>>();
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
        /// <param name="destroyJobAction">全部线程销毁后执行</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<T> jobAction, Action destroyJobAction,
            int duration = 0) : this(
            maxThread,
            jobAction,
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
            Guid guid = Guid.NewGuid();
            _taskBaseCommon.AddJob(new TaskJobBaseParam<T>(guid, item));

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
                this._tasks.ForEach(task => { task.Key.Start(); });
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
                this._tasks.Add(new KeyValuePair<Task, Stopwatch>(new Task(StartJob), Stopwatch.StartNew()));
            }
        }

        #endregion

        #region 执行任务

        /// <summary>
        /// 执行任务
        /// </summary>
        private void StartJob()
        {
            if (this._taskBaseCommon.GetNextJob(out TaskJobBaseParam<T> jobParam) && this.IsRun)
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
                taskInfo.Value?.Stop();
                if (this._continueTimer != -1)
                {
                    if (taskInfo.Value?.ElapsedMilliseconds < this._continueTimer)
                    {
                        taskInfo.Value?.Start();
                        StartJob();
                    }
                    else
                    {
                        RemoveTask(taskInfo);
                        lock (this._tasks)
                        {
                            if (this._tasks.Count == 0)
                            {
                                this._destroyTaskAction?.Invoke();
                            }
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
                if (this._continueTimer != -1)
                {
                    taskInfo.Value?.Reset();
                }

                StartJob();
            }
        }

        #region 得到当前线程任务

        /// <summary>
        /// 得到当前线程任务
        /// </summary>
        /// <returns></returns>
        private KeyValuePair<Task, Stopwatch> GetTaskInfo()
        {
            lock (this._tasks)
            {
                return this._tasks.FirstOrDefault(x => x.Key.Id == Task.CurrentId);
            }
        }

        #endregion

        #region 移除当前线程任务

        /// <summary>
        /// 移除当前线程任务
        /// </summary>
        /// <param name="task"></param>
        private void RemoveTask(KeyValuePair<Task, Stopwatch> task)
        {
            lock (this._tasks)
            {
                this._tasks.Remove(task);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
