using Api.Base;
using Dominio.Entidades.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplications.Interfaces;
using System;
using System.Threading.Tasks;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constantes.UriPorDefectoWebApi + "[controller]")]
    [ApiController]
    public class SecurityController : HandlerBaseLiteController<UsuarioAplicacion>
    {
        private readonly ISeguridadService _seguridadService;
        public SecurityController(ISeguridadService seguridadService)
        {
            _seguridadService = seguridadService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> InicialSesion(Login login)
        {
            return this.ManejadorRespuesta(await this._seguridadService.IniciarSesion(login));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(Registro registro)
        {
            return this.ManejadorRespuesta(await this._seguridadService.RegistrarUsuario(registro));
        }

        [Authorize]
        [HttpPut]
        [Route("actualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario(UsuarioAplicacion usuario)
        {
            return this.ManejadorRespuesta(await this._seguridadService.UpdateUser(usuario));
        }

        [Authorize]
        [HttpDelete]
        [Route("eliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(string user)
        {
            return this.ManejadorRespuesta(await this._seguridadService.DeleteUser(user));
        }

        [Authorize]
        [HttpPost]
        [Route("registrarRol")]
        public async Task<IActionResult> RegistrarRol(string rol)
        {
            return this.ManejadorRespuesta(await this._seguridadService.CreateRol(rol));
        }

        [Authorize]
        [HttpGet]
        [Route("obtenerRoles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            return this.ManejadorRespuesta(await this._seguridadService.ToListRol());
        }

        [Authorize]
        [HttpDelete]
        [Route("eliminarRol")]
        public async Task<IActionResult> EliminarRol(string rol)
        {
            return this.ManejadorRespuesta(await this._seguridadService.DeleteRol(rol));
        }

    }
}
