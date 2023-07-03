using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Application.Sections.Commands.Delete
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteSectionCommandHandler(IMediator mediator, IMapper mapper, IApplicationDbContext context)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
