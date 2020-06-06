// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 基础类
    /// </summary>
    internal class TaskBaseCommon
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        public int _maxThread;

        /// <summary>
        /// 等待中的任务
        /// </summary>
        public readonly Hashtable AwaitList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 进行中的任务
        /// </summary>
        public readonly Hashtable OnGoingList = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 无任务后休息的时间
        /// </summary>
        public readonly int _duration;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        /// <param name="duration">默认无任务后休息3000ms</param>
        public TaskBaseCommon(int maxThread, int duration = 0)
        {
            _maxThread = maxThread;
            _duration = duration;
            Check.True(_duration >= 0, "duration设置有误");
        }

        /// <summary>
        /// 得到最大线程数
        /// </summary>
        public int GetMaxThread => _maxThread;

        #region private methods

        #region 判断是否可开启新的任务

        /// <summary>
        /// 判断是否可开启新的任务
        /// </summary>
        public bool IsStartNewProcess => OnGoingList.Count < _maxThread;

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
