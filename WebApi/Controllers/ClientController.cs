using Application.Clients.Commands.Create;
using Application.Clients.Queries.GetClient;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        /// Добавить клиента
        /// </summary>
        /// <param name="Данные о клиенте"></param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="500">Клиент уже существует</response>
        [HttpPost]
        public async Task<ActionResult> CreateClient([FromForm] CreateClientDto request)
        {
            var command = _mapper.Map<CreateClientCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Найти клиента по номеру телефона
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <returns>Возвращает модель клиента</returns>
        /// /// <response code="204">Выполнено успешно</response>
        /// <response code="500">Клиент не найден</response>
        [HttpGet]
        public async Task<ActionResult> GetClient(string phone)
        {
            var query = new GetClientQuery { Phone = phone };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}