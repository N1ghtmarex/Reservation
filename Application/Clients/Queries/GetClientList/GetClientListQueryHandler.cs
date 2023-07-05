using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetClientList
{
    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients
                .Skip(request.Offset)
                .Take(request.Limit)
                .ProjectTo<ClientVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ClientListVm { ClientProfile = clients };
        }
    }
}
