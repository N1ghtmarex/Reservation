using Application.Interfaces;
using Domain.Entities;
using MediatR;

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
            await _context.Sections.AddAsync(new Section
            {
                Name = request.Name,
                SportId = request.SportId,
                CoachId = request.CoachId,
                RoomId = request.RoomId
                
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
