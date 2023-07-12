using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Queries.GetRoomList
{
    internal class GetRoomListQueryHandler : IRequestHandler<GetRoomListQuery, RoomListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomListVm> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
                .ProjectTo<RoomVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new RoomListVm { Rooms = rooms };
        }
    }
}
