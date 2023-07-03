using MediatR;

namespace Application.Clients.Commands.Create
{
    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
