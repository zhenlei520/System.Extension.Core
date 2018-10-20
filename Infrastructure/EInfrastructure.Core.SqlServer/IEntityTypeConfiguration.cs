using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.SqlServer
{
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}