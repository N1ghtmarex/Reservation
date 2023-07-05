namespace Domain.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid SportId { get; set; }
        public Sport Sport { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public Guid CoachId { get; set; }
        public Coach Coach { get; set; }

        public List<Client>? Client { get; set; } = new List<Client>();

        public List<SectionReservation>? SectionReservation { get; set; } 
    }
}
