using Domain.Common;
using Domain.Entities;
using Domain.Port;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util.Ex;

namespace ServiceApplication
{

    public class ElevatorService : BaseServiceApplication<Elevator, ElevatorDto>, IBaseServiceApplication<Elevator, ElevatorDto>, IElevatorService
    {


        private readonly IElevatorMovementService _elevatorMovementService;

        public ElevatorService(IElevatorRepository elevatorRepository,IElevatorMovementService elevatorMovementService)
            : base(elevatorRepository)
        {
            _elevatorMovementService = elevatorMovementService;
            CreateMapperExpresion<Elevator, ElevatorDto>(cnf =>
            {
                ElevatorMapper.Expresion(cnf);
            });
        }

        public async Task MoveToFloor(int floors,bool up,ElevatorDto elevator)
        {
            for (int i = 1; i <= floors; i++)
            {
                elevator.LastFloor = (up ? Up(elevator.LastFloor) : Down(elevator.LastFloor));
                UpdateModel(elevator);
                await Task.Delay(1000);
                Console.WriteLine($"Status: Actual Floor { elevator.LastFloor }");
            }
        }

        public async Task<ElevatorStatusDto> GetStatus(string codeElevator)
        {
           

            var movement = _elevatorMovementService.MapLstToDTO<ElevatorMovement,ElevatorMovementDto>(
                                         await _elevatorMovementService.ToListModelBy
                                                (t => t.ElevatorCode==codeElevator &&
                                                t.Status == States.Active.ToString()));

            var elevator = await FirstOrDefautlModelBy(f => f.Code == codeElevator);
            return new ElevatorStatusDto()
            {
                CodeElevtor=elevator.Code,
                FloorActual=elevator.LastFloor,
                MovementHP= movement.ToList().Where(w=>w.Priority==1).ToList()?? new System.Collections.Generic.List<ElevatorMovementDto>(),
                MovementLP = movement.ToList().Where(w => w.Priority == 2).ToList() ?? new System.Collections.Generic.List<ElevatorMovementDto>(),
                Status=elevator.Status
            };

        }
        private int Up(int actual)
        {
            return actual += 1;
        }

        private int Down(int actual)
        {
            return actual -= 1;
        }

        public bool IsUp(int initial,int final)
        {
            if (initial < final)
                return true;
            else
                return false;
        }
    }
}
