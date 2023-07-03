using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sports.Queries.GetSport
{
    public class GetSportQueryHandler : IRequestHandler<GetSportQuery, SportVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SportVm> Handle(GetSportQuery request, CancellationToken cancellationToken)
        {
            var sport = await _context.Sports.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (sport != null)
                return _mapper.Map<SportVm>(sport);
            else
                throw new NotFoundException("Спорт", request.Name);
        }
    }
}
