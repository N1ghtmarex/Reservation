using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservation;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class GetIndividualReservationDto : IMapWith<GetIndividualReservationQuery>
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
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
