using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Sports.Commands.Create
{
    public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSportCommandHandler(IApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task Handle(CreateSportCommand request, CancellationToken cancellationToken)
        {
            await _context.Sports.AddAsync(new Sport
            {
                Name = request.Name
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
