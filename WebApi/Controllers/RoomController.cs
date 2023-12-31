﻿using Application.Rooms.Commands.Create;
using Application.Rooms.Queries.GetRoomList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить зал
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromForm] string name)
        {
            var command = new CreateRoomCommand { Name = name };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получить список залов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<RoomListVm>> GetRooms()
        {
            var query = new GetRoomListQuery { };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
