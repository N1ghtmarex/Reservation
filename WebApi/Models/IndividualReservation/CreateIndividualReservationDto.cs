using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Commands.Create;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class CreateIndividualReservationDto : IMapWith<CreateIndividualReservationCommand>
    {
        /// <summary>
        /// День недели
        /// </summary>
        public string DayOfWeek { get; set; } = string.Empty;
        /// <summary>
        /// Время в формате ЧЧ:ММ
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// Телефон тренера
        /// </summary>
        public string CoachPhone { get; set; } = string.Empty;
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string SportName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateIndividualReservationDto, CreateIndividualReservationCommand>();
        }
    }
}
