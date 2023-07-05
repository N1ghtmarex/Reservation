using MediatR;

namespace Application.Clients.Queries.GetClientList
{
    public class GetClientListQuery : IRequest<ClientListVm>
    {
        /// <summary>
        /// Количество клиентов
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Смещение от начала
        /// </summary>
        public int Offset { get; set; }
    }
}
