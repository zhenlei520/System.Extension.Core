using System;
using System.Threading.Tasks;

namespace EInfrastructure.Core.HelpCommon
{
    public static class TaskExtend
    {
        #region Public Methods

        public static void ToTaskNoResult(Action action)
        {
            Task.Run(action);
        }

        public static Task ToTask(Action action)
        {
            return Task.Run(action);
        }
        public static Task<T> ToTask<T>(Func<T> function)
        {
            return Task.Run(function);
        }
        public static async Task ToTaskAsync(Action action)
        {
            await Task.Run(action);
        }
        public static async Task<T> ToTaskAsync<T>(Func<T> function)
        {
            return await Task.Run(function);
        }

        public static void TaskNoResult<T>(Func<T> function)
        {
            Task.Run(function);
        }

        #endregion Public Methods

        #region 扩展Task

        public static async void ToTaskAsyncExt(Action action)
        {
            await Task.Run(action);
        }

        #endregion
    }
}
