using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Rooms.Queries
{
    public class RoomVm : IMapWith<Room>
    {
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Room, RoomVm>();
        }
    }
}
