using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using MediatR;

namespace Application.Records.IndividualRecords.Queries.GetWeekIndividualRecordsList
{
    public class GetWeekIndividualRecordsListQuery : IRequest<IndividualReservationListVm>
    {
        public Guid ClientId { get; set; }
        public required string StartDate { get; set; }
    }
}
