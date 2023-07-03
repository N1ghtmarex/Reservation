using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sports.Commands.Delete
{
    public class DeleteSportCommandHandler : IRequestHandler<DeleteSportCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteSportCommandHandler(IMediator mediator, IMapper mapper, IApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteSportCommand request, CancellationToken cancellationToken)
        {
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (sport != null)
            {
                _context.Sports.Remove(sport);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
                throw new NotFoundException("Спорт", request.Name);
        }
    }
}
