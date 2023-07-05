using Application.Sports.Commands.Create;
using Application.Sports.Commands.Delete;
using Application.Sports.Queries;
using Application.Sports.Queries.GetSport;
using Application.Sports.Queries.GetSportsList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/sports")]
    public class SportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SportController(IMediator mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить вид спорта
        /// </summary>
        /// <param name="name">Вид спорта</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        [HttpPost]
        public async Task<ActionResult> CreateSport([FromForm] string name)
        {
            var command = new CreateSportCommand { Name = name};

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Удалить вид спорта
        /// </summary>
        /// <param name="name">Вид спорта</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="500">Сущность не найдена</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteSport([FromForm] string name)
        {
            var command = new DeleteSportCommand { Name = name };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить вид спорта
        /// </summary>
        /// <param name="name">Вид спорта</param>
        /// <returns>Возвращает модель спорта</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="500">Сущность не найдена</response>
        [HttpGet]
        public async Task<ActionResult<SportVm>> GetSport(string name)
        {
            var query = new GetSportQuery { Name = name };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        ///  Получить список видов спорта
        /// </summary>
        /// <param name="limit">Ограничение по количеству возвращаемых видов спорта</param>
        /// <param name="offset">Смещение от начала</param>
        /// <returns>Возвращает список видов спорта</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("{limit}/{offset}")]
        public async Task<ActionResult<SportsListVm>> GetSportsList(int limit, int offset)
        {
            var query = new GetSportsListQuery 
            { 
                Limit = limit,
                Offset = offset 
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
