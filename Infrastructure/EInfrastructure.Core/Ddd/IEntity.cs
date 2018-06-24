using System;

namespace EInfrastructure.Core.Ddd
{
    public interface IEntity<T>
    {
        T Id
        {
            get;
        }

    }
}
