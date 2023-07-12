using MediatR;

namespace Application.Reservations.SectionReservations.Queries.GetSectionReservationList
{
    public class GetSectionReservationListQuery : IRequest<SectionReservationListVm>
    {
        public Guid ClientId { get; set; }
        public required string Date { get; set; }
    }
}
