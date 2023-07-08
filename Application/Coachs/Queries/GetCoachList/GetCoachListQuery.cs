using MediatR;

namespace Application.Coachs.Queries.GetCoachList
{
    public class GetCoachListQuery : IRequest<CoachListVm>
    {
        /// <summary>
        /// Количество тренеров
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Смещение от начала
        /// </summary>
        public int Offset { get; set; }
    }
}
