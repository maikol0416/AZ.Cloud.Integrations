using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;
using System.Collections.Generic;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class UserMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf, IRolService service)
        {

            RolMapper.Expresion(cnf);


            cnf.CreateMap<UserDto, User>()
                .ConstructUsing(s => s != null ? new User
                    (s.UserName, s.Email, s.Nombre, s.Password, service.MapLstToENT<Rol, RolDto>(s.Roles)) : null);

            cnf.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(x => string.Empty))
                ;

        }

       
    }

}

