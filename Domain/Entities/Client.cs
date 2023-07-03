namespace Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = string.Empty;

        public List<Section>? Section { get; set; }

        public List<IndividualRecord>? IndividualRecord { get; set; }
    }
}
