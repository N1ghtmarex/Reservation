using Application.Reservations.IndividualReservations.Commands.Create;
using Application.Reservations.IndividualReservations.Queries;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservation;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
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

        [HttpGet("individual")]
        public async Task<ActionResult<IndividualReservationVm>> GetIndividualReservation([FromQuery] GetIndividualReservationDto request)
        {
            var query = _mapper.Map<GetIndividualReservationQuery>(request);

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("individual/{day}")]
        public async Task<ActionResult<IndividualReservationListVm>> GetIndividualReservationList(string day)
        {
            var query = new GetIndividualReservationListQuery { DayOfWeek = day};

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
