using Api.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceApplications.Interfaces;
using System;
using System.Threading.Tasks;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constantes.UriPorDefectoWebApi + "[controller]")]
    [ApiController]
    public class PortfolioMovementController : HandlerBaseController<PortfolioMovement,IPortfolioMovementService>
    {

        public PortfolioMovementController(IPortfolioMovementService PortfolioMovementService)
           : base(PortfolioMovementService)
        {

        }
    }
}
