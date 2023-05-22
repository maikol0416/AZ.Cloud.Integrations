
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ElevatorRepository : RepositoryBase<Elevator>, IRepositoryBase<Elevator>, IElevatorRepository
    {
        public ElevatorRepository(IMainContextCosmos mainContext)
            : base(mainContext)
        {

        }
    }
}
