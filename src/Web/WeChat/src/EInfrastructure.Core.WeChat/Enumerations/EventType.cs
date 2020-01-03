// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.WeChat.Enumeration
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public class EventType : Core.Config.EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static EventType Unknow = new EventType(0, "", "未知");

        /// <summary>
        /// 订阅
        /// </summary>
        public static EventType Subscribe = new EventType(1, "subscribe", "订阅");

        /// <summary>
        /// 取消订阅
        /// </summary>
        public static EventType Unsubscribe = new EventType(2, "unsubscribe", "取消订阅");

        public EventType(int id, string name, string desc) : base(id, name)
        {
            Desc = desc;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; private set; }
    }
}
