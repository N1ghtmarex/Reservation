using MediatR;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList
{
    public class GetIndividualReservationListQuery : IRequest<IndividualReservationListVm>
    {
        public string? Date { get; set; }
        public string? Time { get; set; }
        public Guid? SportId { get; set; }
    }
}
