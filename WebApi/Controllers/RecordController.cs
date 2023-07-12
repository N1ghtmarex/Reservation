using Application.Records.IndividualRecords.Commands.Create;
using Application.Records.IndividualRecords.Queries.GetIndividualRecordsList;
using Application.Records.IndividualRecords.Queries.GetSectionRecordsList;
using Application.Records.IndividualRecords.Queries.GetWeekIndividualRecordsList;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using Application.Reservations.SectionReservations.Queries.GetSectionReservationList;
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
        public async Task<ActionResult> CreateRecord(string id)
        {
            var command = new CreateIndividualRecordCommand
            {
                ClientId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                ReservationId = Guid.Parse(id)
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Расписание индивидуальных событий на день
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{date}")]
        [Authorize]
        public async Task<ActionResult<IndividualReservationListVm>> GetRecords(string date)
        {
            var query = new GetIndividualRecordsListQuery
            {
                ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                Date = date
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Расписание секции на неделю
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("sections/{date}")]
        [Authorize]
        public async Task<ActionResult<SectionReservationListVm>> GetWeekSectionRecords(string date)
        {
            var query = new GetWeekSectionRecordsListQuery
            {
                ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                StartDate = date
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Расписание секции на день
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        [HttpGet("sections/day={day}")]
        [Authorize]
        public async Task<ActionResult<SectionReservationListVm>> GetDaySectionRecords(string day)
        {
            var query = new GetSectionReservationListQuery
            {
                ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                Date = day
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Расписание индивидуальных занятий на неделю
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("individual/week={date}")]
        [Authorize]
        public async Task<ActionResult<IndividualReservationListVm>> GetWeekIndividualRecords(string date)
        {
            var query = new GetWeekIndividualRecordsListQuery
            {
                ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                StartDate = date
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
