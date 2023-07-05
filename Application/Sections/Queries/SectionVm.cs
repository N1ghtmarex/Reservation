using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Sections.Queries
{
    public class SectionVm : IMapWith<Section>
    {
        public string Name { get; set; } = string.Empty;
        public Coach Coach { get; set; }
        public Sport Sport { get; set; }
        public Room Room { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Section, SectionVm>();
        }
    }
}
