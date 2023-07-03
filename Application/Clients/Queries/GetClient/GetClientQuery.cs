using MediatR;

namespace Application.Clients.Queries.GetClient
{
    public class GetClientQuery : IRequest<ClientVm>
    {
        public string Phone { get; set; }
    }
}
