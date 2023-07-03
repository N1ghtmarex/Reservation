using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(coach => coach.ID);
            builder.HasIndex(coach => coach.ID).IsUnique();
            builder.Property(coach => coach.Name).IsRequired().HasMaxLength(20);
            builder.Property(coach => coach.Surname).IsRequired().HasMaxLength(30);
            builder.Property(coach => coach.Patronymic).HasMaxLength(30);
            builder.Property(coach => coach.Phone).IsRequired().HasMaxLength(11);
        }
    }
}
