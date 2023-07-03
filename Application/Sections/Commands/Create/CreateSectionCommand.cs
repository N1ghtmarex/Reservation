using Domain.Entities;
using MediatR;

namespace Application.Sections.Create
{
    public class CreateSectionCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid SportID { get; set; }
    }
}
