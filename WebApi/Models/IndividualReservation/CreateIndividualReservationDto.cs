using Application.Common.Mappings;
using Application.Reservations.IndividualReservations.Commands.Create;
using AutoMapper;

namespace WebApi.Models.IndividualReservation
{
    public class CreateIndividualReservationDto
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
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
