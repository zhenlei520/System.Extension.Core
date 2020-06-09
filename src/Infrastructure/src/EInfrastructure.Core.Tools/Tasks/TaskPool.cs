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
        private readonly TaskBaseCommon _taskBaseCommon;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息0ms</param>
        private TaskPool(int maxThread, int duration = 0)
        {
            this._taskBaseCommon = new TaskBaseCommon(maxThread, duration);
            this._tasks = new List<TaskStateRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="action"></param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<object> action, int duration = 0) : this(maxThread, duration)
        {
            this._jobAction = action;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="jobAction"></param>
        /// <param name="finishAction"></param>
        /// <param name="duration">默认无任务后休息0ms</param>
        public TaskPool(int maxThread, Action<object> jobAction, Action finishAction, int duration = 0) : this(
            maxThread,
            jobAction,
            duration)
        {
            this._finishAction = finishAction;
        }

        /// <summary>
        /// 工作任务
        /// </summary>
        private Action<object> _jobAction;

        /// <summary>
        /// 完成后执行
        /// </summary>
        private Action _finishAction;

        /// <summary>
        ///
        /// </summary>
        private List<TaskStateRequest> _tasks;

        /// <summary>
        /// 是否启动
        /// </summary>
        private bool IsRun;

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        public void AddJob(T item)
        {
            Guid guid = Guid.NewGuid();
            _taskBaseCommon.AwaitList.Add(guid, new TaskJobBaseParam<T>(guid, item));
        }

        /// <summary>
        ///
        /// </summary>
        public void Run()
        {
            if (!IsRun)
            {
                IsRun = true;
                Start();
            }
        }

        private void Start()
        {
            Task.Run(() =>
            {
                while (this._taskBaseCommon.IsCanProcess)
                {
                    StartProcessForWait();
                }
            });
        }

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

        #region 执行等待中的任务

        #region 开始执行等待进行的任务

        /// <summary>
        /// 开始执行等待进行的任务
        /// </summary>
        private void StartProcessForWait()
        {
            if (_taskBaseCommon.AwaitList.Count > 0 && _taskBaseCommon.IsCanProcess)
            {
                TaskJobBaseParam<T> taskJobParam = _taskBaseCommon.GetNextJob2<T>();
                if (taskJobParam != null)
                {
                    var task = GetTask(taskJobParam.Id);
                    StartProcess(task, taskJobParam).ContinueWith(taskNow =>
                    {
                        if (_taskBaseCommon.OnGoingList.ContainsKey(taskNow.Result))
                        {
                            _taskBaseCommon.OnGoingList.Remove(taskNow);
                        }

                        var taskInfo = this._tasks.FirstOrDefault(x => x.Task.Result == taskNow.Result);
                        taskInfo?.Stop();
                        StartProcessForWait();
                        return taskNow.Result;
                    });
                }
            }
            else
            {
                //无任务
                if (_taskBaseCommon._duration > 0)
                {
                    Thread.Sleep(_taskBaseCommon._duration);
                }

                StartProcessForWait();
            }
        }

        #endregion

        #region 开始执行任务

        /// <summary>
        /// 开始执行任务
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="item"></param>
        private Task<Guid> StartProcess(Task<Guid> task, TaskJobBaseParam<T> item)
        {
            if (!_taskBaseCommon.OnGoingList.ContainsKey(item.Id))
            {
                _taskBaseCommon.OnGoingList.Add(item.Id, item.Data);
            }
            else
            {
                return task;
            }

            return task.ContinueWith(taskNow =>
            {
                var taskInfo = this._tasks.FirstOrDefault(x => x.Task.Result == item.Id);
                taskInfo?.Run();
                this._jobAction.Invoke(item);
                return item.Id;
            });
        }

        #endregion

        #region MyRegion

        #endregion

        #region 得到空闲的线程任务

        /// <summary>
        /// 得到空闲的线程任务
        /// </summary>
        /// <returns></returns>
        private Task<Guid> GetTask(Guid id)
        {
            lock (this._tasks)
            {
                var task = this._tasks.Where(x => x.IsFree).Select(x => x.Task).FirstOrDefault();
                if (task == null)
                {
                    task = Task.Factory.StartNew(() => { return id; });
                    this._tasks.Add(new TaskStateRequest(task));
                }

                return task;
            }
        }

        #endregion

        #endregion
    }
}
