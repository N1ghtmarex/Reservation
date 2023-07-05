using MediatR;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservation
{
    public class GetIndividualReservationQuery : IRequest<IndividualReservationVm>
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string CoachPhone { get; set; } = string.Empty;
        public string SportName { get; set; } = string.Empty;
    }
}
