using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sections.Commands.AddClient
{
    internal class AddClientToSectionCommandHandler : IRequestHandler<AddClientToSectionCommand>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public AddClientToSectionCommandHandler(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(AddClientToSectionCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId);

            if (client == null) 
            {
                throw new NotFoundException("Клиент", "");
            }

            var section = _context.Sections
                .Where(x => x.Id == request.SectionId)
                .Include(x => x.Sport)
                .Include(x => x.Room)
                .Include(x => x.Coach)
                .Include(x => x.SectionReservation)
                .FirstOrDefaultAsync(cancellationToken).Result;

            if (section == null)
            {
                throw new NotFoundException("Секция", "");
            }
            else
            {
                if (client.Section == null)
                {
                    var sections = new List<Section>
                {
                    section
                };

                    client.Section = sections;
                }
                else
                {
                    client.Section.Add(section);
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
            
        }
    }
}
