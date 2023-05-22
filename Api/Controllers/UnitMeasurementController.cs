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
    public class UnitMeasurementController : HandlerBaseController<UnitMeasurement,IUnitMeasurementService>
    {

        public UnitMeasurementController(IUnitMeasurementService UnidadMedidaService)
           : base(UnidadMedidaService)
        {

        }
    }
}
