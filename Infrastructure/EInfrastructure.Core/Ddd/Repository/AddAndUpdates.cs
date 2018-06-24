using System;

namespace EInfrastructure.Core.Ddd.Repository
{
  /// <summary>
  /// 添加并且修改聚合根
  /// </summary>
  public class AddAndUpdate<T> : Adds<T>
  {
    public AddAndUpdate()
    {
      EditTime = DateTime.Now;
    }

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
