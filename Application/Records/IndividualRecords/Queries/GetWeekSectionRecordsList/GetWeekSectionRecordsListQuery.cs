using Application.Reservations.SectionReservations.Queries.GetSectionReservationList;
using MediatR;

namespace Application.Records.IndividualRecords.Queries.GetSectionRecordsList
{
    public class GetWeekSectionRecordsListQuery : IRequest<SectionReservationListVm>
    {
        public Guid ClientId { get; set; }
        public required string StartDate { get; set; }
    }
}
