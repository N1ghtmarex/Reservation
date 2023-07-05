namespace Domain.Entities
{
    public class Sport
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Section> Section { get; set; }

        public List<IndividualReservation> IndividualReservation { get; set; }
    }
}
