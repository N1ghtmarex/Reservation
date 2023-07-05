namespace Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Section> Section { get; set; }
    }
}
