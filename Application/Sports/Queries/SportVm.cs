using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Sports.Queries
{
    public class SportVm : IMapWith<Sport>
    {
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sport, SportVm>();
        }
    }
}
