
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ElevatorMovementRepository : RepositoryBase<ElevatorMovement>, IRepositoryBase<ElevatorMovement>, IElevatorMovementRepository
    {
        public ElevatorMovementRepository(IMainContextCosmos mainContext)
            : base(mainContext)
        {

        }
    }
}
