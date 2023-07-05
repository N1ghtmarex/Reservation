using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservation;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class GetIndividualReservationDto : IMapWith<GetIndividualReservationQuery>
    {
        /// <summary>
        /// День недели
        /// </summary>
        public string DayOfWeek { get; set; } = string.Empty;
        /// <summary>
        /// Время в формате ЧЧ:ММ
        /// </summary>
        public string Time { get; set; } = string.Empty;
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
            profile.CreateMap<GetIndividualReservationDto, GetIndividualReservationQuery>();
        }
    }
}
