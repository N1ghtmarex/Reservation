using MediatR;

namespace Application.Sports.Queries.GetSportsList
{
    public class GetSportsListQuery : IRequest<SportsListVm>
    {   
        /// <summary>
        /// Ограничение по количеству возвращаемых видов спорта
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Смещение от начала
        /// </summary>
        public int Offset { get; set; }
    }
}
