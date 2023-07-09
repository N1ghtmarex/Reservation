using MediatR;

namespace Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList
{
    public class GetIndividualReservationListQuery : IRequest<IndividualReservationListVm>
    {
        public string Date { get; set; }
    }
}
