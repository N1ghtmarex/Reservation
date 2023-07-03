using MediatR;

namespace Application.Sections.Commands.Delete
{
    public class DeleteSectionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
