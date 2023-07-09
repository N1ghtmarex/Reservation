using MediatR;

namespace Application.Reservations.SectionReservations.Queries.GetSectionReservationList
{
    public class GetSectionReservationListQuery : IRequest<SectionReservationListVm>
    {
        public required int DayOfWeek { get; set; }
    }
}
