using Application.Common.Mappings;
using Application.Sections.Create;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Models.Sections
{
    public class CreateSectionDto : IMapWith<CreateSectionCommand>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string SportName { get; set; } = string.Empty;
        /// <summary>
        /// Телефон тренера
        /// </summary>
        public string CoachPhone { get; set; } = string.Empty;
        /// <summary>
        /// Зал
        /// </summary>
        public string RoomName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSectionDto, CreateSectionCommand>();
        }
    }
}
