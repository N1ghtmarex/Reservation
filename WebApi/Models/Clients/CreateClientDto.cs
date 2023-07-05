using Application.Clients.Commands.Create;
using Application.Common.Mappings;
using AutoMapper;

namespace WebApi.Models.Clients
{
    public class CreateClientDto : IMapWith<CreateClientCommand>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateClientDto, CreateClientCommand>();
        }
    }
}
