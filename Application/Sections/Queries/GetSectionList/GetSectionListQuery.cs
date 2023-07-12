using MediatR;

namespace Application.Sections.Queries.GetSectionList
{
    public class GetSectionListQuery : IRequest<SectionListVm>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
