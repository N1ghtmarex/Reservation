using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coachs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<IndividualRecord> IndividualRecords { get; set; }
        public DbSet<IndividualReservation> IndividualReservations { get; set; }
        public DbSet<SectionReservation> SectionReservations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
