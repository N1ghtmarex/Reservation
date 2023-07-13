using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.SectionReservations.Commands.Create
{
    public class CreateSectionReservationCommand : IRequest
    {
        public int DayOfWeek { get; set; }
        public required string Time { get; set; }
        public required string Duration { get; set; }
        public string Period { get; set; }
        public Guid SectionId { get; set; }
    }
}
