/**
 * 调用实例
public class Test
{
    public void Main()
    {
        try
        {
            TimeCommonExt timeOut = new TimeCommonExt(
                delegate
                {
                    try
                    {
                        //需要在指定时间内完成的方法
                    }
                    catch (Exception)
                    {
                        //方法出错调用
                    }
                }, delegate
                {
                    //方法超时未完成调用
                }
                );
            timeOut.Wait(3000);//方法的超时时间,单位为毫秒
        }
        catch (Exception)
        {
            //方法出错调用
        }
    }
}
 */

using System;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 方法 (超时自动结束)
    /// </summary>
    public class TimeCommonExt
    {
        long _timeout;              //超时时间  
        Action<Delegate> _proc;               //会超时的代码  
        Action<Delegate> _procHandle;         //处理超时  
        Action<Delegate> _timeoutHandle;      //超时后处理事件  
        System.Threading.ManualResetEvent _event = new System.Threading.ManualResetEvent(false);

        /// <summary>
        /// 超时自动结束
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="timeoutHandle"></param>
        public TimeCommonExt(Action<Delegate> proc, Action<Delegate> timeoutHandle)
        {
            _proc = proc;
            _timeoutHandle = timeoutHandle;
            _procHandle = delegate
            {
                //计算代码执行的时间  
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                _proc?.Invoke(null);
                sw.Stop();
                //如果执行时间小于超时时间则通知用户线程  
                if (sw.ElapsedMilliseconds < _timeout)
                {
                    _event?.Set();
                }
            };
        }

        #region 设置等待时间
        /// <summary>
        /// 设置等待时间
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool Wait(long timeout)
        {
            _timeout = timeout;
            //异步执行  
            _procHandle.BeginInvoke(null, null, null);
            //如果在规定时间内没等到通知则为 false  
            bool flag = _event.WaitOne((int)timeout, false);
            if (!flag)
            {
                //触发超时时间  
                if (_timeoutHandle != null)
                    _timeoutHandle(null);
            }
            Dispose();

            return flag;
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        private void Dispose()
        {
            if (_event != null)
                _event.Close();
            _event = null;
            _proc = null;
            _procHandle = null;
            _timeoutHandle = null;
        }
        #endregion
    }
}
