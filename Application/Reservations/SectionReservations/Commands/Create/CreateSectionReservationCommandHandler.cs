using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.SectionReservations.Commands.Create
{
    internal class CreateSectionReservationCommandHandler : IRequestHandler<CreateSectionReservationCommand>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public CreateSectionReservationCommandHandler(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(CreateSectionReservationCommand request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(x => x.Name.ToLower() == request.SectionName.ToLower());

            if (section == null) 
            {
                throw new NotFoundException("Секция", "Name = " + request.SectionName);
            }

            var period = DateOnly.ParseExact(request.Period, "dd-MM-yyyy");
            var startTime = TimeOnly.Parse(request.Time);
            var duration = TimeOnly.Parse(request.Duration);
            var endTime = startTime.AddHours(duration.Hour).AddMinutes(duration.Minute);

            var reservation = new SectionReservation
            {
                DayOfWeek = request.DayOfWeek,
                StartTime = startTime,
                EndTime = endTime,
                Period = period,
                SectionId = section.Id
            };

            await _context.SectionReservations.AddAsync(reservation, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
