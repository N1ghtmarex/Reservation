using AutoMapper;
using MediatR;

namespace Application.Sections.Queries.GetSection
{
    public class GetSectionQuery : IRequest<SectionVm>
    {
        public string Name { get; set; } = string.Empty;
    }
}
