using Application.Common.Mappings;
using Application.Sections.Create;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Models.Sections
{
    public class CreateSectionDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор спорта
        /// </summary>
        public string SportId { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор зала
        /// </summary>
        public string RoomId { get; set; } = string.Empty;
    }
}
