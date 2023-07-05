using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Coachs.Queries
{
    public class CoachVm : IMapWith<Coach>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Coach, CoachVm>();
        }
    }
}
