using MediatR;

namespace Application.Coachs.Commands.Create
{
    public class CreateCoachCommand : IRequest
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }

    }
}
