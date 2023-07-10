using Application.Records.IndividualRecords.Commands.Create;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/records")]
    public class RecordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RecordController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Записаться на индивидуальное событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> CreateRecord(Guid id)
        {
            var command = new CreateIndividualRecordCommand
            {
                ClientId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                ReservationId = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
