using Application.Common.Mappings;
using Application.Sports.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Clients.Queries
{
    public class ClientVm : IMapWith<Client>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientVm>();
        }
    }
}
