namespace Domain.Entities
{
    public class SectionReservation
    {
        public Guid Id { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public DateOnly Period { get; set; } 

        public Guid CoachId { get; set; }
        public Coach Coach { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }
    }
}
