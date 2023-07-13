using MediatR;

namespace Application.Reservations.IndividualReservations.Commands.Create
{
    public class CreateIndividualReservationCommand : IRequest
    {
        public Guid Id { get; set; }
        public required string Date { get; set; }
        public required string Duration { get; set; }
        public Guid SportId { get; set; }
    }
}
