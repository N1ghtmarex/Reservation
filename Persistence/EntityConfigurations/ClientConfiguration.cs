using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder) 
        {
            builder.HasKey(client => client.Id);
            builder.HasIndex(client => client.Id).IsUnique();
            builder.Property(client => client.Name).IsRequired().HasMaxLength(20);
            builder.Property(client => client.Surname).IsRequired().HasMaxLength(30);
            builder.Property(client => client.Patronymic).HasMaxLength(30);
            builder.Property(client => client.Phone).IsRequired().HasMaxLength(11);
        }
    }
}
