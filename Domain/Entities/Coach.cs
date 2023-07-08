namespace Domain.Entities
{
    public class Coach
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }

        public List<Section>? Section { get; set; } = new List<Section>();

        public List<IndividualReservation>? IndividualReservation { get; set; }

        public List<SectionReservation>? SectionReservation { get; set; }
    }
}
