using Application.Coachs.Queries;
using Application.Common.Mappings;
using Application.Rooms.Queries;
using Application.Sports.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Sections.Queries
{
    public class SectionVm : IMapWith<Section>
    {
        public string Name { get; set; } = string.Empty;
        public CoachVm Coach { get; set; }
        public SportVm Sport { get; set; }
        public RoomVm Room { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Section, SectionVm>();
        }
    }
}
