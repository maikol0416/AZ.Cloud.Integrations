using System;
using System.Threading.Tasks;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication
{
	public interface IElevatorService:IBaseServiceApplication<Elevator, ElevatorDto>
	{
        Task<ElevatorStatusDto> GetStatus(string codeElevator);
        bool IsUp(int initial, int final);
        Task MoveToFloor(int floors, bool up, ElevatorDto elevator);
    }
}

