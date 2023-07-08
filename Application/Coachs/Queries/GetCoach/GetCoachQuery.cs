using MediatR;

namespace Application.Coachs.Queries.GetCoach
{
    public class GetCoachQuery : IRequest<CoachVm>
    {
        public required Guid Id { get; set; }
    }
}
