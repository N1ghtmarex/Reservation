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
        /// Id спорта
        /// </summary>
        public Guid SportId { get; set; }
        /// <summary>
        /// Id тренера
        /// </summary>
        public Guid CoachId { get; set; }
        /// <summary>
        /// Id зала
        /// </summary>
        public Guid RoomId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSectionDto, CreateSectionCommand>();
        }
    }
}
