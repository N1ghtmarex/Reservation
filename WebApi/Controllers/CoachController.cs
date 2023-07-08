using Application.Coachs.Commands.Create;
using Application.Coachs.Commands.Login;
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
    }
}
