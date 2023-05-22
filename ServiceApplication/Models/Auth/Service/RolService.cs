using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;

namespace ServiceApplication
{
    public class RolService : BaseServiceApplication<Rol, RolDto>, IBaseServiceApplication<Rol, RolDto>, IRolService
    {
        public RolService(IRolRepository repositorioBase)
            : base(repositorioBase)
        {
            CreateMapperExpresion<Rol, RolDto>(cnf =>
            {
                RolMapper.Expresion(cnf);
            });
        }
    }
}
