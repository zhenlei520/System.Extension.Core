using System;

namespace EInfrastructure.Core.Ddd.Repository
{
  /// <summary>
  /// 添加信息聚合根
  /// </summary>
  public class Adds<T> : AggregateRoot<T>
  {
    public Adds()
    {
      AddTime = DateTime.Now;
    }

    public Adds(T accountId)
    {
      AddAccountId = accountId;
      AddTime = DateTime.Now;
    }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime AddTime { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public T AddAccountId { get; set; }
  }
}
