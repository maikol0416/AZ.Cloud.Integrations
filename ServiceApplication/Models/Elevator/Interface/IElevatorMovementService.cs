using System;
using System.Threading.Tasks;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication
{
	public interface IElevatorMovementService : IBaseServiceApplication<ElevatorMovement,ElevatorMovementDto>
	{
		
	}
}

