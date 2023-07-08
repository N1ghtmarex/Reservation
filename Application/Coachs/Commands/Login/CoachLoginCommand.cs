using MediatR;

namespace Application.Coachs.Commands.Login
{
    public class CoachLoginCommand : IRequest<string>
    {
        /// <summary>
        /// Телефон
        /// </summary>
        public required string Phone { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
