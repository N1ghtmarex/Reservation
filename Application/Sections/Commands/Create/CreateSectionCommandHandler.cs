using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SportId = request.SportID
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
