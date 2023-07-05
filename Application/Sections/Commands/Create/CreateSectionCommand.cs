using MediatR;

namespace Application.Sections.Create
{
    public class CreateSectionCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid SportId { get; set; }
        public Guid CoachId { get; set; }
        public Guid RoomId { get; set;}
    }
}
