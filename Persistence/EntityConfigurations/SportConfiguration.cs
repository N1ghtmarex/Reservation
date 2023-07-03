using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class SportConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder) 
        {
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.ID);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        }
    }
}
