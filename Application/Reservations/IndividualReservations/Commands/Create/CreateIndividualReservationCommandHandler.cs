using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Id == request.Id);
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Id == request.SportId);

            if (coach == null)
                throw new NotFoundException("Тренер", "Phone = " + request.Id);

            if (sport == null)
                throw new NotFoundException("Спорт", request.SportId);

            var date = DateTime.ParseExact(request.Date, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture).ToUniversalTime();
            var duration = TimeOnly.Parse(request.Duration);
            var endDate = date.AddHours(duration.Hour).AddMinutes(duration.Minute);

            if (await _context.IndividualReservations.FirstOrDefaultAsync(x => x.Date == date && x.CoachId == coach.Id && x.SportId == sport.Id) != null)
            {
                throw new AlreadyExistsException("Событие", "");
            }

            var reservation = new IndividualReservation
            {
                Date = date,
                EndDate = endDate,
                CoachId = coach.Id,
                SportId = sport.Id
            };

            await _context.IndividualReservations.AddAsync(reservation);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
