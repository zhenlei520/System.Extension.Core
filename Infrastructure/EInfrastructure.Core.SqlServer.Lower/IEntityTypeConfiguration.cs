using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.SqlServer.Lower
{
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}