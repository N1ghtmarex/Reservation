using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class GetIndividualReservationListDto
    {
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? SportId { get; set; }

    }
}
