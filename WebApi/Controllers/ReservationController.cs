using Application.Reservations.IndividualReservations.Commands.Create;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.IndividualReservation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReservationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("individual")]
        public async Task<ActionResult> CreateIndividualReservation([FromForm] CreateIndividualReservationDto request)
        {
            var command = _mapper.Map<CreateIndividualReservationCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
