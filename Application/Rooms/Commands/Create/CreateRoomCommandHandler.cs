using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Commands.Create
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Clients.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken) != null)
                throw new AlreadyExistsException("Зал", request.Name);

            var room = new Room { Name = request.Name };

            await _context.Rooms.AddAsync(room);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
