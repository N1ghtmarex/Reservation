using Application.Interfaces;
using Application.Sections.Commands.Delete;
using Application.Sections.Create;
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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SectionController(IApplicationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSection([FromForm] CreateSectionDto request)
        {
            var command = _mapper.Map<CreateSectionCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSection([FromForm] Guid Id)
        {
            var command = new DeleteSectionCommand { Id = Id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
