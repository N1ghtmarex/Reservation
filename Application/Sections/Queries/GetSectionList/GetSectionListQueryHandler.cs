using Application.Coachs.Queries;
using Application.Interfaces;
using Application.Reservations.IndividualReservations.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sections.Queries.GetSectionList
{
    internal class GetSectionListQueryHandler : IRequestHandler<GetSectionListQuery, SectionListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSectionListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectionListVm> Handle(GetSectionListQuery request, CancellationToken cancellationToken)
        {
            var sections = await _context.Sections
                .Skip(request.Offset)
                .Take(request.Limit)
                .ProjectTo<SectionVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new SectionListVm { Sections = sections };
        }
    }
}
