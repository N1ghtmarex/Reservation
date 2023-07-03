namespace Domain.Entities
{
    public class IndividualRecord
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid IndividualReservationId { get; set; }
        public IndividualReservation IndividualReservation { get; set; }
    }
}
