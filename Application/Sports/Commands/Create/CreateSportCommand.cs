using MediatR;

namespace Application.Sports.Commands.Create
{
    public class CreateSportCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
