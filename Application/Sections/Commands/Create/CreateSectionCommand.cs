using MediatR;

namespace Application.Sections.Create
{
    public class CreateSectionCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string SportName { get; set; }
        public string CoachPhone { get; set; }
        public string RoomName { get; set;}
    }
}
