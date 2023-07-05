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

        public CreateCoachCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCoachCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.Phone) != null)
                throw new NotFoundException("Тренер", "Phone = " + request.Phone);

            var coach = new Coach
            {
                Name = request.Name,
                Surname = request.Surname,
                Phone = request.Phone
            };

            if (request.Patronymic != null)
                coach.Patronymic = request.Patronymic;

            await _context.Coachs.AddAsync(coach);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
