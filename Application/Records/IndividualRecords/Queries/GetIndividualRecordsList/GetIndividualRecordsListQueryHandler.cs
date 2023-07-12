using Application.Interfaces;
using Application.Reservations.IndividualReservations.Queries;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Records.IndividualRecords.Queries.GetIndividualRecordsList
{
    internal class GetIndividualRecordsListQueryHandler : IRequestHandler<GetIndividualRecordsListQuery, IndividualReservationListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetIndividualRecordsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IndividualReservationListVm> Handle(GetIndividualRecordsListQuery request, CancellationToken cancellationToken)
        {
            var date = DateOnly.ParseExact(request.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var records = await _context.IndividualRecords
                .Where(x => x.ClientId == request.ClientId)
                .Select(x => x.IndividualReservation)
                    .Where(x => DateOnly.FromDateTime(x.Date) == date)
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            records.ForEach(delegate (IndividualReservationVm reservation) {
                reservation.Date = TimeZoneInfo.ConvertTimeFromUtc(reservation.Date, TimeZoneInfo.Local);
                reservation.EndDate = TimeZoneInfo.ConvertTimeFromUtc(reservation.EndDate, TimeZoneInfo.Local);
            });

            return new IndividualReservationListVm { IndividualReservations = records };
        }
    }
}
