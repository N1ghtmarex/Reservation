namespace WebApi.Models.IndividualReservation
{
    public class CreateIndividualReservationDto
    {
        /// <summary>
        /// Дата
        /// </summary>
        public string reservationDate { get; set; }
        /// <summary>
        /// Продолжительность
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string SportName { get; set; } = string.Empty;
    }
}
