using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sports.Queries.GetSportsList
{
    public class GetSportsListQueryHandler : IRequestHandler<GetSportsListQuery, SportsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSportsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SportsListVm> Handle(GetSportsListQuery request, CancellationToken cancellationToken)
        {
            var sports = await _context.Sports
                .ProjectTo<SportVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new SportsListVm { Sports = sports };
        }
    }
}
