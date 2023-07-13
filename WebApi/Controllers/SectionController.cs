using Application.Interfaces;
using Application.Sections.Commands.AddClient;
using Application.Sections.Commands.Delete;
using Application.Sections.Create;
using Application.Sections.Queries.GetSectionForRecord;
using Application.Sections.Queries.GetSectionList;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<ActionResult> CreateSection([FromBody] CreateSectionDto request)
        {
            var command = new CreateSectionCommand
            {
                CoachId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                Name = request.Name,
                RoomId = Guid.Parse(request.RoomId),
                SportId = Guid.Parse(request.SportId)
            };

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
        /// Получить секцию для записи
        /// </summary>
        /// <returns>Модель секции</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("free")]
        public async Task<ActionResult> GetSectionForRecord()
        {
            var query = new GetSectionForRecordQuery { ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value) };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("add-to-section={sectionId}")]
        [Authorize]
        public async Task<ActionResult> AddClientToSection(string sectionId)
        {
            var command = new AddClientToSectionCommand
            {
                ClientId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                SectionId = Guid.Parse(sectionId)
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить список секций
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<SectionListVm>> GetSections()
        {
            var query = new GetSectionListQuery {};

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
