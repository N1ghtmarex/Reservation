using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sections.Create
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSectionCommandHandler(IApplicationDbContext context) 
        {  
            _context = context; 
        }

        public async Task Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Id == request.CoachId);
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Id == request.SportId);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId);

            if (coach == null)
                throw new NotFoundException("Тренер", "Phone = " + request.CoachId);

            if (sport == null)
                throw new NotFoundException("Спорт", request.SportId);

            if (room == null)
                throw new NotFoundException("Зал", request.RoomId);

            await _context.Sections.AddAsync(new Section
            {
                Name = request.Name,
                SportId = sport.Id,
                CoachId = coach.Id,
                RoomId = room.Id,

            }, cancellationToken); ;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
