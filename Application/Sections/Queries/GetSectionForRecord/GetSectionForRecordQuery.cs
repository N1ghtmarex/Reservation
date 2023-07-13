using Application.Sections.Queries.GetSectionList;
using MediatR;

namespace Application.Sections.Queries.GetSectionForRecord
{
    public class GetSectionForRecordQuery : IRequest<SectionListVm>
    {
        public Guid ClientId { get; set; }
    }
}
