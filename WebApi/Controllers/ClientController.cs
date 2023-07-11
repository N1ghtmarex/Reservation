using Application.Clients.Commands.Create;
using Application.Clients.Commands.Login;
using Application.Clients.Queries.GetClient;
using Application.Clients.Queries.GetClientList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.Clients;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClientController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Войти в аккаунт клиента
        /// </summary>
        /// <param name="request">Данные клиента</param>
        /// <returns>Возвращает токен доступа</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpPost("client")]
        public async Task<ActionResult<string>> Auth([FromBody] ClientLoginDto request)
        {
            var command = _mapper.Map<ClientLoginCommand>(request);

            var token = await _mediator.Send(command);

            return Ok(token);
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="request">Данные о клиенте</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="500">Клиент уже существует</response>
        [HttpPost]
        public async Task<ActionResult> CreateClient([FromBody] CreateClientDto request)
        {
            var command = _mapper.Map<CreateClientCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить данные профиля
        /// </summary>
        /// <returns>Возвращает модель клиента</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="500">Клиент не найден</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetClient()
        {
            var query = new GetClientQuery { Id = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value)};

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <param name="limit">Ограничение по количеству возвращаемых клиентов</param>
        /// <param name="offset">Смещение от начала</param>
        /// <returns>Возвращает список клиентов</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("{limit}/{offset}")]
        public async Task<ActionResult> GetClients(int limit, int offset)
        {
            var query = new GetClientListQuery
            {
                Limit = limit,
                Offset = offset
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}