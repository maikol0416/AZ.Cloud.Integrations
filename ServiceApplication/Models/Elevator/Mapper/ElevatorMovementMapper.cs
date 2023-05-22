using System;
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class ElevatorMovementMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {


            cnf.CreateMap<ElevatorMovementDto, ElevatorMovement>()
                .ConstructUsing(s => s != null ? new ElevatorMovement
                    (s.Floor,s.Priority,s.Code,s.ElevatorCode) : null);

            cnf.CreateMap<ElevatorMovement, ElevatorMovementDto>();

            //cnf.CreateMap<ElevatorMovement, ElevatorMovementDto>()
            //  .ForMember(dest => dest.Floor, opt => opt.MapFrom(x => x.Floor))
            //    .ForMember(dest => dest.Priority, opt => opt.MapFrom(x => x.Priority))
            //        .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status))
            //            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
            //                .ForMember(dest => dest.Code, opt => opt.MapFrom(x => x.Code));

        }
    }
}
