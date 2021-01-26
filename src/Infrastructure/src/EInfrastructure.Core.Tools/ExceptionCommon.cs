// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections;
using EInfrastructure.Core.Tools.Systems;

 namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 异常帮助类
    /// </summary>
    public static class ExceptionCommon
    {
        #region 提取异常及其内部异常堆栈跟踪

        /// <summary>
        /// 提取异常及其内部异常堆栈跟踪
        /// </summary>
        /// <param name="exception">提取的例外</param>
        /// <returns>Syste.String</returns>
        public static string ExtractAllStackTrace(this Exception exception)
        {
            return ExtractAllStackTrace(exception, null, 1);
        }

        /// <summary>
        /// 提取异常及其内部异常堆栈跟踪
        /// </summary>
        /// <param name="exception">提取的例外</param>
        /// <param name="lastStackTrace">最后提取的堆栈跟踪（对于递归）， String.Empty or null</param>
        /// <param name="exCount">提取的堆栈数（对于递归）</param>
        /// <returns>Syste.String</returns>
        private static string ExtractAllStackTrace(this Exception exception, string lastStackTrace,
            int exCount)
        {
            var ex = exception;
            const string entryFormat = "#{0}: {1}\r\n{2}";
            //修复最后一个堆栈跟踪参数
            lastStackTrace = lastStackTrace.SafeString();
            //添加异常的堆栈跟踪
            lastStackTrace += string.Format(entryFormat, exCount, ex.Message, ex.StackTrace);
            if (exception.Data.Count > 0)
            {
                lastStackTrace += "\r\n    Data: ";
                foreach (var item in exception.Data)
                {
                    var entry = (DictionaryEntry) item;
                    lastStackTrace += string.Format("\r\n\t{0}: {1}", entry.Key, exception.Data[entry.Key]);
                }
            }

            //递归添加内部异常
            if ((ex = ex.InnerException) != null)
                return ex.ExtractAllStackTrace(string.Format("{0}\r\n\r\n", lastStackTrace), ++exCount);
            return lastStackTrace;
        }

        #endregion
    }
}
