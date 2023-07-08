using Application.Coachs.Commands.Create;
using Application.Coachs.Commands.Login;
using Application.Coachs.Queries;
using Application.Coachs.Queries.GetCoach;
using Application.Coachs.Queries.GetCoachList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        /// <summary>
        /// Зарегистрировать тренера
        /// </summary>
        /// <param name="request">Данные о тренере</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        [HttpPost]
        public async Task<ActionResult> CreateCoach([FromForm] CreateCoachDto request)
        {
            var command = _mapper.Map<CreateCoachCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Войти в аккаунт тренера
        /// </summary>
        /// <param name="request">Данные тренера</param>
        /// <returns>Возвращает токен доступа</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpPost("coach")]
        public async Task<ActionResult<string>> LoginCoach([FromForm] CoachLoginDto request)
        {
            var command = _mapper.Map<CoachLoginCommand>(request);

            var token = await _mediator.Send(command);

            return Ok(token);
        }

        /// <summary>
        /// Получить данные профиля
        /// </summary>
        /// <returns>Возвращает модель тренера</returns>
        /// /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="500">Тренер не найден</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CoachVm>> GetCoach()
        {
            var query = new GetCoachQuery { Id = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value) };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получить список тренеров
        /// </summary>
        /// <param name="limit">Ограничение по количеству возвращаемых тренеров</param>
        /// <param name="offset">Смещение от начала</param>
        /// <returns>Возвращает список клиентов</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Не авторизован</response>
        [HttpGet("{limit}/{offset}")]
        [Authorize]
        public async Task<ActionResult<CoachListVm>> GetCoachs(int limit, int offset)
        {
            var query = new GetCoachListQuery 
            { 
                Limit = limit, 
                Offset = offset 
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
