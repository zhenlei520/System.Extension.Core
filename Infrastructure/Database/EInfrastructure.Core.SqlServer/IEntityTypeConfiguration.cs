using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}