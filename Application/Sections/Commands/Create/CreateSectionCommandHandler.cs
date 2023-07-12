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
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.CoachPhone);
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Name.ToLower() == request.SportName.ToLower());
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Name.ToLower() == request.RoomName.ToLower());

            if (coach == null)
                throw new NotFoundException("Тренер", "Phone = " + request.CoachPhone);

            if (sport == null)
                throw new NotFoundException("Спорт", request.SportName);

            if (room == null)
                throw new NotFoundException("Зал", request.SportName);

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
