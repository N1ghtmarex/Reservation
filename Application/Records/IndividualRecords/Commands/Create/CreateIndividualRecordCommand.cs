using MediatR;

namespace Application.Records.IndividualRecords.Commands.Create
{
    public class CreateIndividualRecordCommand : IRequest
    {
        public Guid ClientId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
