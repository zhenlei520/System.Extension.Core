namespace EInfrastructure.Core.Ddd
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">实体主键类型</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        T Id { get; }
    }
}