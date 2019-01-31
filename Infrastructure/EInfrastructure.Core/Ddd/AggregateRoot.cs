namespace EInfrastructure.Core.Ddd
{
    /// <summary>
    /// 聚合跟实现
    /// </summary>
    /// <typeparam name="T">主键类型</typeparam>
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public AggregateRoot()
        {
            Id = default(T);
        }
    }
}