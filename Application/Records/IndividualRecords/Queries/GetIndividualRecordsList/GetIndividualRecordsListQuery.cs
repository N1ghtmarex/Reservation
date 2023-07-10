using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using MediatR;

namespace Application.Records.IndividualRecords.Queries.GetIndividualRecordsList
{
    public class GetIndividualRecordsListQuery : IRequest<IndividualReservationListVm>
    {
        public Guid ClientId { get; set; }
        public required string Date { get; set; }
    }
}
