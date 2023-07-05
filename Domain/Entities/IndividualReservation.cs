namespace Domain.Entities
{
    public class IndividualReservation
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly Time { get; set; }

        public Guid CoachId { get; set; }
        public Coach Coach { get; set; }

        public Guid SportId { get; set; }
        public Sport Sport { get; set; }

        public List<IndividualRecord> IndividualRecord { get; set; }
    }
}
