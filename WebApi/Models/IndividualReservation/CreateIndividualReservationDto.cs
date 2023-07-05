using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Commands.Create;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class CreateIndividualReservationDto : IMapWith<CreateIndividualReservationCommand>
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public string Time { get; set; }
        public string CoachPhone { get; set; } = string.Empty;
        public string SportName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateIndividualReservationDto, CreateIndividualReservationCommand>();
        }
    }
}
