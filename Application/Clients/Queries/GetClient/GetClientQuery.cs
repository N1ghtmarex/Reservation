using MediatR;

namespace Application.Clients.Queries.GetClient
{
    public class GetClientQuery : IRequest<ClientVm>
    {
        public required Guid Id { get; set; }
    }
}
