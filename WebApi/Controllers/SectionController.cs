using Application.Interfaces;
using Application.Sections.Commands.Delete;
using Application.Sections.Create;
using Application.Sections.Queries.GetSection;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Sections;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/sections")]
    public class SectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SectionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить секцию
        /// </summary>
        /// <param name="request">Данные секции</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        [HttpPost]
        public async Task<ActionResult> CreateSection([FromForm] CreateSectionDto request)
        {
            var command = _mapper.Map<CreateSectionCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Удалить секцию
        /// </summary>
        /// <param name="Id">ID секции</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteSection([FromForm] Guid Id)
        {
            var command = new DeleteSectionCommand { Id = Id };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить данные о секции
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns>Модель секции</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet]
        public async Task<ActionResult> GetSection(string name)
        {
            var query = new GetSectionQuery { Name = name };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
