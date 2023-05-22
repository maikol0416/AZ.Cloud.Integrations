using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class RolRepository : RepositoryBase<Rol>, IRepositoryBase<Rol>, IRolRepository
    {
        public RolRepository(IMainContextCosmos mainContext)
            : base(mainContext)
        {

        }
    }
}
