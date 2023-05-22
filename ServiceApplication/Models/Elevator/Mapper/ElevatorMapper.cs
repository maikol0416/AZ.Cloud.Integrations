using System;
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class ElevatorMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {


            cnf.CreateMap<ElevatorDto, Elevator>()
                .ConstructUsing(s => s != null ? new Elevator
                    (s.Code,s.LastFloor) : null);


            cnf.CreateMap<Elevator, ElevatorDto>();

        }
    }
}
