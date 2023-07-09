using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class GetIndividualReservationListDto : IMapWith<GetIndividualReservationListQuery>
    {
        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetIndividualReservationListDto, GetIndividualReservationListQuery>();
        }
    }
}
