namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly Time { get; set; }

        public Guid CoachId { get; set; }
        public Coach Coach { get; set; }
    }
}
