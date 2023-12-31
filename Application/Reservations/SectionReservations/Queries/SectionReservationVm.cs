﻿using Application.Common.Mappings;
using Application.Sections.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Reservations.SectionReservations.Queries
{
    public class SectionReservationVm : IMapWith<SectionReservation>
    {
        public Guid Id { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Period { get; set; }
        public SectionVm Section { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SectionReservation, SectionReservationVm>();
        }
    }
}
