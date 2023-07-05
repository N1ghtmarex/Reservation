using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservation;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class GetIndividualReservationDto : IMapWith<GetIndividualReservationQuery>
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string CoachPhone { get; set; } = string.Empty;
        public string SportName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetIndividualReservationDto, GetIndividualReservationQuery>();
        }
    }
}
