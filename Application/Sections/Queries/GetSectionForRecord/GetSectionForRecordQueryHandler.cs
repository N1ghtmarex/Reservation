using Application.Interfaces;
using AutoMapper;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Sections.Queries.GetSectionList;
using AutoMapper.QueryableExtensions;

namespace Application.Sections.Queries.GetSectionForRecord
{
    public class GetSectionForRecordQueryHandler : IRequestHandler<GetSectionForRecordQuery, SectionListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSectionForRecordQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectionListVm> Handle(GetSectionForRecordQuery request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException("Клиент", "");
            }

            var sections = await _context.Sections
                .Where(x => !x.Client.Contains(client))
                .ProjectTo<SectionVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SectionListVm { Sections = sections };
        }

    }
}

