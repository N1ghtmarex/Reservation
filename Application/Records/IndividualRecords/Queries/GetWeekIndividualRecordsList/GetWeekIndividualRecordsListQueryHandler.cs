using Application.Interfaces;
using Application.Reservations.IndividualReservations.Queries;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Records.IndividualRecords.Queries.GetWeekIndividualRecordsList
{
    internal class GetWeekIndividualRecordsListQueryHandler : IRequestHandler<GetWeekIndividualRecordsListQuery, IndividualReservationListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWeekIndividualRecordsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IndividualReservationListVm> Handle(GetWeekIndividualRecordsListQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateOnly.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var endDate = startDate.AddDays(6);

            var records = await _context.IndividualRecords.Select(x => x.IndividualReservation).ToListAsync(cancellationToken);

            var reservations = await _context.IndividualReservations
                .Where(x => (DateOnly.FromDateTime(x.Date) >= startDate && DateOnly.FromDateTime(x.Date) <= endDate) && records.Contains(x))
                .OrderBy(x => x.Date)
                .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            reservations.ForEach(delegate (IndividualReservationVm reservation) {
                reservation.Date = TimeZoneInfo.ConvertTimeFromUtc(reservation.Date, TimeZoneInfo.Local);
                reservation.EndDate = TimeZoneInfo.ConvertTimeFromUtc(reservation.EndDate, TimeZoneInfo.Local);
            });

            return new IndividualReservationListVm { IndividualReservations = reservations };
        }
    }
}
