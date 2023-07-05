using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservation
{
    public class GetIndividualReservationQueryHandler : IRequestHandler<GetIndividualReservationQuery, IndividualReservationVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetIndividualReservationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IndividualReservationVm> Handle(GetIndividualReservationQuery request, CancellationToken cancellationToken)
        {
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.CoachPhone);
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Name.ToLower() == request.SportName.ToLower());
            var time = TimeOnly.Parse(request.Time);

            if (coach == null)
                throw new NotFoundException("Тренер", "Phone = " + request.CoachPhone);

            if (sport == null)
                throw new NotFoundException("Спорт", request.SportName);

            var reservation = await _context.IndividualReservations.FirstOrDefaultAsync(x => x.DayOfWeek.ToLower() == request.DayOfWeek.ToLower() 
                && x.CoachId == coach.Id && x.SportId == sport.Id && x.Time == time);

            if (reservation == null)
                throw new NotFoundException("Событие", "");

            return _mapper.Map<IndividualReservationVm>(reservation);
        }
    }
}
