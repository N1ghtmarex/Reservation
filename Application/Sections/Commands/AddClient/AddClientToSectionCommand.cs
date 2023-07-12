using MediatR;

namespace Application.Sections.Commands.AddClient
{
    public class AddClientToSectionCommand : IRequest
    {
        public Guid ClientId { get; set; }
        public Guid SectionId { get; set; }
    }
}
