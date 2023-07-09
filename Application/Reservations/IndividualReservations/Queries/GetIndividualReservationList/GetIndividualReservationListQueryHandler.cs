using Application.Interfaces;
using AutoMapper;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            var date = DateTime.ParseExact(request.Date, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            var reservations = await _context.IndividualReservations.Where(x => x.Date == date.ToUniversalTime())
                .OrderBy(x => x.Date)
                .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            reservations.ForEach(delegate (IndividualReservationVm reservation) { 
                reservation.Date = TimeZoneInfo.ConvertTimeFromUtc(reservation.Date, TimeZoneInfo.Local);
                reservation.EndDate = TimeZoneInfo.ConvertTimeFromUtc(reservation.EndDate, TimeZoneInfo.Local);
            });

            return new IndividualReservationListVm { IndividualReservations = reservations };
        }
    }
}
