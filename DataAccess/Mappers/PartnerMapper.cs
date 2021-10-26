using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    public class PartnerMapper : IEntityTypeConfiguration<PartnerEntity>
    {
        public void Configure(EntityTypeBuilder<PartnerEntity> builder)
        {
            builder.ToTable("Partners");
        }
    }
}
