using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.MySql
{
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}