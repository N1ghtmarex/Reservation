using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.IndividualReservations.Commands.Create
{
    public class CreateIndividualReservationCommandHandler : IRequestHandler<CreateIndividualReservationCommand>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public CreateIndividualReservationCommandHandler(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(CreateIndividualReservationCommand request, CancellationToken cancellationToken)
        {
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.CoachPhone);
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Name.ToLower() == request.SportName.ToLower());
            var time = TimeOnly.Parse(request.Time);

            if (coach == null)
                throw new NotFoundException("Тренер", "Phone = " + request.CoachPhone);

            if (sport == null)
                throw new NotFoundException("Спорт", request.SportName);


            if (await _context.IndividualReservations.FirstOrDefaultAsync(x => x.DayOfWeek == request.DayOfWeek
                && x.Time == time && x.CoachId == coach.Id && x.SportId == sport.Id) != null)
            {
                throw new AlreadyExistsException("Событие", "");
            }

            var reservation = new IndividualReservation
            {
                DayOfWeek = request.DayOfWeek,
                Time = time,
                CoachId = coach.Id,
                SportId = sport.Id
            };

            await _context.IndividualReservations.AddAsync(reservation);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
