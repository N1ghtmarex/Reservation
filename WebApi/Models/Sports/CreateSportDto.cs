using Application.Common.Mappings;
using Application.Sections.Create;
using Application.Sports.Commands.Create;
using AutoMapper;

namespace WebApi.Models.Sports
{
    public class CreateSportDto : IMapWith<CreateSportCommand>
    {
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSportDto, CreateSportCommand>();
        }
    }
}
