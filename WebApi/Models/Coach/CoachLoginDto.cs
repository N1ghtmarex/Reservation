using Application.Coachs.Commands.Login;
using Application.Common.Mappings;
using AutoMapper;

namespace WebApi.Models.Coach
{
    public class CoachLoginDto : IMapWith<CoachLoginCommand>
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
            profile.CreateMap<CoachLoginDto, CoachLoginCommand>();
        }
    }
}
