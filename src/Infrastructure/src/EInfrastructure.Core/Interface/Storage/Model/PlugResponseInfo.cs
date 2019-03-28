// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Interface.Storage.Model
{
    /// <summary>
    /// 响应数据
    /// </summary>
    public class PlugResponseInfo
    {
        /// <summary>
        /// 响应数据构造函数
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        public PlugResponseInfo(bool status, string msg, object data)
        {
            Status = status;
            Msg = msg;
            Data = data;
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static PlugResponseInfo Success(string msg)
        {
            return new PlugResponseInfo(true, msg, null);
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static PlugResponseInfo Success(object data)
        {
            return new PlugResponseInfo(true, "", data);
        }

        /// <summary>
        /// 失败回调
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static PlugResponseInfo Err(string msg, object data = null)
        {
            return new PlugResponseInfo(false, msg, data);
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
