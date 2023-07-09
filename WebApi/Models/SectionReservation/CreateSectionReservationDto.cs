using Application.Common.Mappings;
using Application.Reservations.SectionReservations.Commands.Create;
using AutoMapper;

namespace WebApi.Models.SectionReservation
{
    public class CreateSectionReservationDto
    {
        public int DayOfWeek { get; set; }
        public required string Time { get; set; }
        public required string Duration { get; set; }
        public string Period { get; set; }
        public required string SectionName { get; set; }
    }
}
