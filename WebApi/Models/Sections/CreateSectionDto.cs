using Application.Common.Mappings;
using Application.Sections.Create;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Models.Sections
{
    public class CreateSectionDto : IMapWith<CreateSectionCommand>
    {
        public string Name { get; set; } = string.Empty;
        public Guid SportID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSectionDto, CreateSectionCommand>();
        }
    }
}
