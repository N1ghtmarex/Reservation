using MediatR;

namespace Application.Reservations.IndividualReservations.Commands.Create
{
    public class CreateIndividualReservationCommand : IRequest
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string CoachPhone { get; set; } = string.Empty;
        public string SportName { get; set; } = string.Empty;
    }
}
