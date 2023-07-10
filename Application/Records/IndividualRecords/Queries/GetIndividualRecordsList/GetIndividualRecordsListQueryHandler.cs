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
            var date = DateTime.ParseExact(request.Date, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            var records = await _context.IndividualRecords
                .Where(x => x.ClientId == request.ClientId)
                .Select(x => x.IndividualReservation)
                    .Where(x => x.Date == date.ToUniversalTime())
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return new IndividualReservationListVm { IndividualReservations = records };
        }
    }
}
