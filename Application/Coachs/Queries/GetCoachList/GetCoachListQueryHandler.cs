using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Coachs.Queries.GetCoachList
{
    internal class GetCoachListQueryHandler : IRequestHandler<GetCoachListQuery, CoachListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCoachListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoachListVm> Handle(GetCoachListQuery request, CancellationToken cancellationToken)
        {
            var coachs = await _context.Coachs
                .Skip(request.Offset)
                .Take(request.Limit)
                .ProjectTo<CoachVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new CoachListVm { Coachs = coachs };
        }
    }
}
