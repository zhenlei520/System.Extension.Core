using System;

namespace EInfrastructure.Core.Ddd
{

  public abstract class Entity<T> : IEntity<T>
  {
    public virtual T Id { get; set; }
  }
}
