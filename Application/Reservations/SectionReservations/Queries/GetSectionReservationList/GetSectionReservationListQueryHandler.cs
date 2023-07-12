using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Reservations.IndividualReservations.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Reservations.SectionReservations.Queries.GetSectionReservationList
{
    internal class GetSectionReservationListQueryHandler : IRequestHandler<GetSectionReservationListQuery, SectionReservationListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSectionReservationListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectionReservationListVm> Handle(GetSectionReservationListQuery request, CancellationToken cancellationToken)
        {
            var date = DateOnly.ParseExact(request.Date, "dd-MM-yyyy");
            var dayOfWeek = (((int)date.DayOfWeek) == 0) ? 7 : (int)date.DayOfWeek;

            var client = await _context.Clients.Where(x => x.Id == request.ClientId)
                .Include(x => x.Section)
                    .ThenInclude(x => x.Sport)
                .Include(x => x.Section)
                    .ThenInclude(x => x.Room)
                .Include(x => x.Section)
                    .ThenInclude(x => x.Coach)
                .Include(x => x.Section)
                    .ThenInclude(x => x.SectionReservation)
                .Include(x => x.IndividualRecord)
                .FirstOrDefaultAsync(cancellationToken);


            if (client == null)
            {
                throw new NotFoundException("Клиент", "");
            }

            if (client.Section == null)
            {
                throw new NotFoundException("Секции", "");
            }
            else
            {
                var sections = client.Section;

                var reservations = await _context.SectionReservations
                    .Include(x => x.Section)
                        .ThenInclude(x => x.Sport)
                    .Include(x => x.Section)
                        .ThenInclude(x => x.Room)
                    .Include(x => x.Section)
                        .ThenInclude(x => x.Coach)
                    .Include(x => x.Section)
                        .ThenInclude(x => x.SectionReservation)
                    .Where(x => sections.Contains(x.Section) && x.DayOfWeek == dayOfWeek && x.Period >= date)
                    .OrderBy(x => x.StartTime)
                    .ProjectTo<SectionReservationVm>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new SectionReservationListVm { SectionReservation = reservations };
            }
        }
    }
}
