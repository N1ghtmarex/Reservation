using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Sports.Commands.Create;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Commands.Create
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Clients.FirstOrDefaultAsync(x => x.Phone == request.Phone, cancellationToken) != null)
                throw new AlreadyExistsException("Клиент", "Phone = " + request.Phone);

            var client = new Client
            {
                Name = request.Name,
                Surname = request.Surname,
                Phone = request.Phone
            };

            if (request.Patronymic != null)
                client.Patronymic = request.Patronymic;

            await _context.Clients.AddAsync(client, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
