using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Records.IndividualRecords.Commands.Create
{
    internal class CreateIndividualRecordCommandHandler : IRequestHandler<CreateIndividualRecordCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateIndividualRecordCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateIndividualRecordCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId);

            if (client == null)
            {
                throw new NotFoundException("Клиент", "");
            }

            var reservation = await _context.IndividualReservations.FirstOrDefaultAsync(x => x.Id == request.ReservationId);

            if (reservation == null)
            {
                throw new NotFoundException("Событие", "");
            }

            var record = new IndividualRecord
            {
                ClientId = client.Id,
                IndividualReservationId = reservation.Id
            };

            await _context.IndividualRecords.AddAsync(record, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
