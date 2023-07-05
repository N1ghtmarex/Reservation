using Application.Interfaces;
using AutoMapper;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList
{
    public class GetIndividualReservationListQueryHandler :IRequestHandler<GetIndividualReservationListQuery, IndividualReservationListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetIndividualReservationListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IndividualReservationListVm> Handle(GetIndividualReservationListQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _context.IndividualReservations.Where(x => x.DayOfWeek.ToLower() == request.DayOfWeek.ToLower())
                .OrderBy(x => x.Time)
                .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new IndividualReservationListVm { IndividualReservations = reservations };
        }
    }
}
