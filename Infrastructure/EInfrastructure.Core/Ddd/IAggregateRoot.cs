namespace EInfrastructure.Core.Ddd
{
    /// <summary>
    /// 聚合跟接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAggregateRoot<T> : IEntity<T>
    {
    }
}
