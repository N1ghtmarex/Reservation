using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coachs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<IndividualRecord> IndividualRecords { get; set; }
        public DbSet<IndividualReservation> IndividualReservations { get; set; }
        public DbSet<SectionReservation> SectionReservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new CoachConfiguration());

            //Настройка связей для секции
            //Отношения между клиентом и секцией
            builder.Entity<Section>()
                .HasMany(s => s.Client)
                .WithMany(c => c.Section)
                .UsingEntity(j => j.ToTable("ClientSection"));

            //Отношения между тренером и секцией
            builder.Entity<Section>()
                .HasOne(s => s.Coach)
                .WithMany(c => c.Section);

            //Отношения между залом и секцией
            builder.Entity<Section>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Section);

            //Отношения между спортом и секцией
            builder.Entity<Section>()
                .HasOne(s => s.Sport)
                .WithMany(s => s.Section);


            //Отношения между событием секции и секцией
            builder.Entity<SectionReservation>()
                .HasOne(sr => sr.Section)
                .WithMany(s => s.SectionReservation);

            //Настройка связей для индивидуальных событий
            //Отношения между тренером и индивидуальным событием
            builder.Entity<IndividualReservation>()
                .HasOne(ir => ir.Coach)
                .WithMany(c => c.IndividualReservation);

            //Отношения между видом спорта и индивидуальным событием
            builder.Entity<IndividualReservation>()
                .HasOne(ir => ir.Sport)
                .WithMany(s => s.IndividualReservation);


            //Настройка связей для индивидуальной записи
            builder.Entity<IndividualRecord>()
                .HasKey(t => new { t.ClientId, t.IndividualReservationId });

            //Отношения между индивидуальным событием и индивидуальной записью
            builder.Entity<IndividualRecord>()
                .HasOne(ir => ir.IndividualReservation)
                .WithMany(ir => ir.IndividualRecord);

            //Отношения между индивидуальным событием и клиентом
            builder.Entity<IndividualRecord>()
                .HasOne(ir => ir.Client)
                .WithMany(c => c.IndividualRecord);
        }
    }
}
