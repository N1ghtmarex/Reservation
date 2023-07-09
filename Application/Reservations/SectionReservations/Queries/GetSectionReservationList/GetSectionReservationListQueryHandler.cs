using Application.Interfaces;
using Application.Reservations.IndividualReservations.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var reservations = await _context.SectionReservations.Where(x => x.DayOfWeek == request.DayOfWeek)
                .OrderBy(x => x.StartTime)
                .ProjectTo<SectionReservationVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new SectionReservationListVm { SectionReservation =  reservations };
        }
    }
}
