using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.Common;

namespace Api.Base
{
    [Authorize]
    public abstract partial class HandlerBaseController<T, TNeogcio>
        where T : class, new()
        where TNeogcio : IBaseServiceApplication<T>
    {
        protected readonly TNeogcio Negocio;
        public HandlerBaseController(TNeogcio negocio)
        {
            this.Negocio = negocio;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Crear(T entidad)
        {
            return this.ManejadorRespuesta(await Negocio.CreateModel(entidad));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Editar(T entidad)
        {
            return this.ManejadorRespuesta(await Negocio.UpdateModel(entidad));
        }

        [HttpGet("get")]
        public async Task<IActionResult> Listar()
        {
            return this.ManejadorRespuesta(await Negocio.TolistModel());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Eliminar(string id)
        {
            return this.ManejadorRespuesta(await Negocio.DeleteModel(id));
        }

        [HttpPost("paginator")]
        public async Task<IActionResult> Paginar(Paginado<T> paginado)
        {
            return this.ManejadorRespuesta<Paginado<T>>(await Negocio.Paginate(paginado));
        }

        [HttpGet("search/{property}/data/{value}")]
        public async Task<IActionResult> GetBy(string property,string value)
        {
            return this.ManejadorRespuesta(await Negocio.SearchModel(property,value));
        }

        [HttpGet("searchList/{property}/data/{value}")]
        public async Task<IActionResult> GetListBy(string property, string value)
        {
            return this.ManejadorRespuesta(await Negocio.SearchListModel(property, value));
        }


        [HttpPost("sync")]
        public async Task<IActionResult> sincronizar(List<T> entidades)
        {
            return this.ManejadorRespuesta(await Negocio.SyncData(entidades));
        }

    }
}
