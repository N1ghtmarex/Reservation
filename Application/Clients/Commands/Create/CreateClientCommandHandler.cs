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
        private readonly IPasswordService _passwordService;

        public CreateClientCommandHandler(IApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Clients.FirstOrDefaultAsync(x => x.Phone == request.Phone, cancellationToken) != null)
                throw new AlreadyExistsException("Клиент", "Phone = " + request.Phone);

            _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var client = new Client
            {
                Name = request.Name,
                Surname = request.Surname,
                Phone = request.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (request.Patronymic != null)
                client.Patronymic = request.Patronymic;

            await _context.Clients.AddAsync(client, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
