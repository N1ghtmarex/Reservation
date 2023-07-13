using Application.Interfaces;
using AutoMapper;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using Application.Common.Exceptions;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList
{
    public class GetIndividualReservationListQueryHandler : IRequestHandler<GetIndividualReservationListQuery, IndividualReservationListVm>
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
            var records = await _context.IndividualRecords
                .Select(x => x.IndividualReservation)
                .ToListAsync(cancellationToken);

            List<IndividualReservationVm> reservations = new();

            if (request.SportId != null)
            {
                var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Id == request.SportId, cancellationToken) ?? throw new NotFoundException("Спорт", "");
            }

            // дата
            if (request.Date != null && request.Time == null && request.SportId == null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && DateOnly.FromDateTime(x.Date) == DateOnly.ParseExact(request.Date, "dd-MM-yyyy"))
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            }
            // дата, время
            else if (request.Date != null && request.Time != null && request.SportId == null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && DateOnly.FromDateTime(x.Date) == DateOnly.ParseExact(request.Date, "dd-MM-yyyy")
                        && TimeOnly.FromDateTime(x.Date) == TimeOnly.ParseExact(request.Time, "HH:mm"))
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            // дата, время, спорт
            else if (request.Date != null && request.Time != null && request.SportId != null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && DateOnly.FromDateTime(x.Date) == DateOnly.ParseExact(request.Date, "dd-MM-yyyy")
                        && TimeOnly.FromDateTime(x.Date) == TimeOnly.ParseExact(request.Time, "HH:mm")
                        && x.SportId == request.SportId)
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            // дата, спорт
            else if (request.Date != null && request.Time == null && request.SportId != null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && DateOnly.FromDateTime(x.Date) == DateOnly.ParseExact(request.Date, "dd-MM-yyyy")
                        && x.SportId == request.SportId)
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            // время
            else if (request.Date == null && request.Time != null && request.SportId == null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && TimeOnly.FromDateTime(x.Date).AddHours(5) == TimeOnly.ParseExact(request.Time, "HH:mm"))
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            // время, спорт
            else if (request.Date == null && request.Time != null && request.SportId != null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                        && TimeOnly.FromDateTime(x.Date) == TimeOnly.ParseExact(request.Time, "HH:mm")
                        && x.SportId == request.SportId)
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            // спорт
            else if (request.Date == null && request.Time == null && request.SportId != null)
            {
                reservations = await _context.IndividualReservations
                    .Where(x => !records.Contains(x)
                    && x.SportId == request.SportId)
                    .OrderBy(x => x.Date)
                    .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                reservations = await _context.IndividualReservations
                .Where(x => !records.Contains(x))
                .OrderBy(x => x.Date)
                .ProjectTo<IndividualReservationVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }

            reservations.ForEach(delegate (IndividualReservationVm reservation)
            {
                reservation.Date = TimeZoneInfo.ConvertTimeFromUtc(reservation.Date, TimeZoneInfo.Local);
                reservation.EndDate = TimeZoneInfo.ConvertTimeFromUtc(reservation.EndDate, TimeZoneInfo.Local);
            });

            return new IndividualReservationListVm { IndividualReservations = reservations };
        }
    }
}
