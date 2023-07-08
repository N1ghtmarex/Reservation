using MediatR;

namespace Application.Clients.Commands.Login
{
    public record ClientLoginCommand(string Phone, string Password) : IRequest<string> { }
}
