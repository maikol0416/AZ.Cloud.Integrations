using Domain.Entities;
using Domain.Port;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util.Ex;

namespace ServiceApplication
{

    public class ElevatorMovementService : BaseServiceApplication<ElevatorMovement, ElevatorMovementDto>, IBaseServiceApplication<ElevatorMovement, ElevatorMovementDto>, IElevatorMovementService
    {


        private readonly IConfiguration _configurate;
        private readonly IElevatorMovementRepository elevatorMovementRepository;

        public ElevatorMovementService(IConfiguration configuration,IElevatorMovementRepository elevatorMovementRepository)
            : base(elevatorMovementRepository)
        {
            _configurate = configuration;
            this.elevatorMovementRepository = elevatorMovementRepository;
            CreateMapperExpresion<ElevatorMovement, ElevatorMovementDto>(cnf =>
            {
                ElevatorMovementMapper.Expresion(cnf);
            });
        }

       
    }
}
