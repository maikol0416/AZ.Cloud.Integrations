using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class RolMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {


            cnf.CreateMap<RolDto, Rol>()
                .ConstructUsing(s => s != null ? new Rol
                    (s.Name, s.Description, s.Root) : null);

            //cnf.CreateMap<RolDto, Rol>();

            cnf.CreateMap<Rol, RolDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                    .ForMember(dest => dest.Root, opt => opt.MapFrom(x => x.Root))
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id));

        }
    }
}
