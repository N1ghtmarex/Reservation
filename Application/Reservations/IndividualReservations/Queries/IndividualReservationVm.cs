using Application.Coachs.Queries;
using Application.Common.Mappings;
using Application.Sports.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Reservations.IndividualReservations.Queries
{
    public class IndividualReservationVm : IMapWith<IndividualReservation>
    {
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public CoachVm Coach { get; set; } = new();
        public SportVm Sport { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualReservation, IndividualReservationVm>();
        }
    }
}
