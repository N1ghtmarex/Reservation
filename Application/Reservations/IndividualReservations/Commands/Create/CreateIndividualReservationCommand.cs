using MediatR;

namespace Application.Reservations.IndividualReservations.Commands.Create
{
    public class CreateIndividualReservationCommand : IRequest
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public required string Duration { get; set; }
        public string SportName { get; set; } = string.Empty;
    }
}
