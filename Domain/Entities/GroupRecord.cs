namespace Domain.Entities
{
    public class GroupRecord
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly Time { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
    }
}
