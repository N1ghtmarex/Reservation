using MediatR;

namespace Application.Rooms.Commands.Create
{
    public class CreateRoomCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
