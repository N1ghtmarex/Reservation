using MediatR;

namespace Application.Sports.Commands.Delete
{
    public class DeleteSportCommand : IRequest
    {
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string Name { get; set; }
    }
}
