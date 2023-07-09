namespace Domain.Entities
{
    public class IndividualReservation
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CoachId { get; set; }
        public Coach Coach { get; set; }

        public Guid SportId { get; set; }
        public Sport Sport { get; set; }

        public List<IndividualRecord> IndividualRecord { get; set; }
    }
}
