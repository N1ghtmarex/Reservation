using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Coachs.Commands.Create
{
    public class CreateCoachCommandHandler : IRequestHandler<CreateCoachCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public CreateCoachCommandHandler(IApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task Handle(CreateCoachCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.Phone) != null)
                throw new AlreadyExistsException("Тренер", "Phone = " + request.Phone);

            _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var coach = new Coach
            {
                Name = request.Name,
                Surname = request.Surname,
                Phone = request.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (request.Patronymic != null)
                coach.Patronymic = request.Patronymic;

            await _context.Coachs.AddAsync(coach);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
