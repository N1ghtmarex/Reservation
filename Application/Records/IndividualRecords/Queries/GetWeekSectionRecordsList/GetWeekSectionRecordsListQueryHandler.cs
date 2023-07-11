using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Records.IndividualRecords.Queries.GetSectionRecordsList;
using Application.Reservations.SectionReservations.Queries;
using Application.Reservations.SectionReservations.Queries.GetSectionReservationList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Records.IndividualRecords.Queries.GetWeekSectionRecordsList
{
    internal class GetWeekSectionRecordsListQueryHandler : IRequestHandler<GetWeekSectionRecordsListQuery, SectionReservationListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWeekSectionRecordsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectionReservationListVm> Handle(GetWeekSectionRecordsListQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateOnly.ParseExact(request.StartDate, "dd-MM-yyyy");
            var endDate = startDate.AddDays(6);
            var endDay = ((int)endDate.DayOfWeek == 0) ? 7 : (int)endDate.DayOfWeek;

            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId);

            if (client == null)
            {
                throw new NotFoundException("Клиент", "");
            }

            var clientSections = await _context.Sections
                .Where(x => x.Client.Contains(client))
                .Include(x => x.Client)
                .Include(x => x.Sport)
                .Include(x => x.Room)
                .Include(x => x.Coach)
                .Include(x => x.SectionReservation)
                .ToListAsync(cancellationToken);

            if (clientSections == null)
            {
                throw new NotFoundException("Секции", "client = " + request.ClientId);
            }
            else
            {
                var reservations = await _context.SectionReservations
                    .Where(x => clientSections.Contains(x.Section) 
                        && ((x.Period <= endDate && x.DayOfWeek < (int)x.Period.DayOfWeek && x.DayOfWeek >= (int)startDate.DayOfWeek) 
                        || (x.Period > endDate && (x.DayOfWeek >= (int)startDate.DayOfWeek || x.DayOfWeek <= (int)endDate.DayOfWeek) )))
                    .OrderBy(x => x.DayOfWeek)
                    .ProjectTo<SectionReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new SectionReservationListVm { SectionReservation = reservations };
            }
        }
    }
}
