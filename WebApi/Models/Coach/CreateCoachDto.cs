using Application.Coachs.Commands.Create;
using Application.Common.Mappings;
using AutoMapper;

namespace WebApi.Models.Coach
{
    public class CreateCoachDto : IMapWith<CreateCoachCommand>
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
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCoachDto, CreateCoachCommand>();
        }
    }
}
