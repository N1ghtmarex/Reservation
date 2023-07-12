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

            var section = await _context.Sections.FirstOrDefaultAsync(x => x.Id == request.SectionId);

            if (section == null)
            {
                throw new NotFoundException("Секция", "");
            }

            if (client.Section == null)
            {
                var sections = new List<Section>();
                sections.Add(section);

                client.Section = sections;
            }
            else
            {
                client.Section.Add(section);
            }
            
            _context.Clients.Update(client);

            _context.SaveChangesAsync(cancellationToken);
        }
    }
}
