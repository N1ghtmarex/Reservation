using Application.Clients.Commands.Login;
using Application.Common.Mappings;
using AutoMapper;

namespace WebApi.Models.Clients
{
    /// <summary>
    /// Dto модель для аторизации клиента
    /// </summary>
    public class ClientLoginDto : IMapWith<ClientLoginCommand>
    {
        /// <summary>
        /// Телефон
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClientLoginDto, ClientLoginCommand>();
        }
    }
}
