namespace EInfrastructure.Core.Ddd
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>
    {
        public AggregateRoot()
        {
            Id = default(T);
        }
    }
}
