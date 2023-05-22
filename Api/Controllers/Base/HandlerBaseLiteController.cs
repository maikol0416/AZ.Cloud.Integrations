
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace Api.Base
{
    //[Authorize]
    public class HandlerBaseLiteController<T> : Controller
    {

        protected IActionResult ManejadorRespuesta<T>(T dato)
        {
            return this.Ok(new ResponseApi<T> { Datos = dato, Estado = true, Mensaje = "Operación realizada con exito." });
        }
    }   
}
