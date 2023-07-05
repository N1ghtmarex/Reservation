using Application.Coachs.Commands.Create;
using Application.Common.Mappings;
using AutoMapper;

namespace WebApi.Models.Coach
{
    public class CreateCoachDto : IMapWith<CreateCoachCommand>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCoachDto, CreateCoachCommand>();
        }
    }
}
