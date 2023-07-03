using MediatR;

namespace Application.Sports.Queries.GetSport
{
    public class GetSportQuery : IRequest<SportVm>
    {
        public string Name { get; set; }
    }
}
