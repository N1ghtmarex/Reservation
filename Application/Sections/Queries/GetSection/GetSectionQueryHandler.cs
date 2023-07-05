using Application.Interfaces;
using AutoMapper;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.Sections.Queries.GetSection
{
    public class GetSectionQueryHandler : IRequestHandler<GetSectionQuery, SectionVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSectionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectionVm> Handle(GetSectionQuery request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections
                .Include(x => x.Coach)
                .Include(x => x.Room)
                .Include(x => x.Sport)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (section != null)
                return _mapper.Map<SectionVm>(section);
            else
                throw new NotFoundException("Секция", request.Name);
        }
    }
}

