using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Coachs.Queries.GetCoach
{
    internal class GetCoachQueryHandler : IRequestHandler<GetCoachQuery, CoachVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCoachQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoachVm> Handle(GetCoachQuery request, CancellationToken cancellationToken)
        {
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (coach == null) 
            {
                throw new NotFoundException("Тренер", "");
            }

            return _mapper.Map<CoachVm>(coach);
        }
    }
}
