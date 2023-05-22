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
    public class TaxtController : HandlerBaseController<Taxt,ITaxtService>
    {

        public TaxtController(ITaxtService IvaService)
           : base(IvaService)
        {

        }
    }
}
