using System;

namespace EInfrastructure.Core.Ddd.Repository
{
    /// <summary>
    /// 添加并且修改聚合根
    /// </summary>
    public class AddAndUpdate<T> : Adds<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AddAndUpdate() : base()
        {
            EditTime = DateTime.Now;
        }

        /// <summary>
        /// 构造函数（默认为更新用户以及添加用户赋值）
        /// </summary>
        /// <param name="accountId"></param>
        public AddAndUpdate(T accountId) : base(accountId)
        {
            EditAccountId = accountId;
            EditTime = DateTime.Now;
        }

        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="accountId">账户id</param>
        public void UpdateInfo(T accountId)
        {
            EditAccountId = accountId;
            EditTime = DateTime.Now;
        }

        /// <summary>
        /// 修改用户id
        /// </summary>
        public T EditAccountId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
    }
}