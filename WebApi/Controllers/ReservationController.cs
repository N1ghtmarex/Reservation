using Application.Records.IndividualRecords.Commands.Create;
using Application.Reservations.IndividualReservations.Commands.Create;
using Application.Reservations.IndividualReservations.Queries;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservation;
using Application.Reservations.IndividualReservations.Queries.GetIndividualReservationList;
using Application.Reservations.SectionReservations.Commands.Create;
using Application.Reservations.SectionReservations.Queries.GetSectionReservationList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.IndividualReservation;
using WebApi.Models.SectionReservation;

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

        /// <summary>
        /// Создать индивидуальное событие
        /// </summary>
        /// <param name="request">Данные о событии</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        [HttpPost("individual")]
        [Authorize]
        public async Task<ActionResult> CreateIndividualReservation([FromForm] CreateIndividualReservationDto request)
        {
            var coachId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var command = new CreateIndividualReservationCommand
            {
                Id = coachId,
                Date = request.Date,
                Duration = request.Duration,
                SportName = request.SportName
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить индивидуальное событие
        /// </summary>
        /// <param name="request">Данные о событии</param>
        /// <returns>Возвращает модель события</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("individual")]
        public async Task<ActionResult<IndividualReservationVm>> GetIndividualReservation([FromQuery] GetIndividualReservationDto request)
        {
            var query = _mapper.Map<GetIndividualReservationQuery>(request);

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получить индивидуальное событие по дням
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Возвращает список событий за указанный день</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("individual/{date}")]
        public async Task<ActionResult<IndividualReservationListVm>> GetIndividualReservationList(string date)
        {
            var query = new GetIndividualReservationListQuery { Date = date };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Добавить событие секции
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("section")]
        [Authorize]
        public async Task<ActionResult> CreateSectionReservation([FromForm] CreateSectionReservationDto request)
        {
            var command = new CreateSectionReservationCommand
            {
                DayOfWeek = request.DayOfWeek,
                Duration = request.Duration,
                Period = request.Period,
                SectionName = request.SectionName,
                Time = request.Time
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить события секций по дням
        /// </summary>
        /// <param name="day">День недели</param>
        /// <returns></returns>
        [HttpGet("section/{day}")]
        public async Task<ActionResult<SectionReservationListVm>> GetSectionReservations(int day)
        {
            var query = new GetSectionReservationListQuery { DayOfWeek = day };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
