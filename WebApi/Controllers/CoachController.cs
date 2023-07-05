using Application.Coachs.Commands.Create;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Coach;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/coachs")]
    public class CoachController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CoachController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCoach([FromForm] CreateCoachDto request)
        {
            var command = _mapper.Map<CreateCoachCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
