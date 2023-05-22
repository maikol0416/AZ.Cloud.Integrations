
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class SecurityRepository : RepositoryBase<User>, IRepositoryBase<User>, ISecurityRepository
    {
        public SecurityRepository(IMainContextCosmos mainContext)
            : base(mainContext)
        {

        }
    }
}
