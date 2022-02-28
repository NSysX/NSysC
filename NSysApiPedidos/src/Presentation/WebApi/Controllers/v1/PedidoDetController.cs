using Application.DTOs;
using Application.Features.PedidosDet.Commands.ActualizarPedidosCommand;
using Application.Features.PedidosDet.Commands.EliminarPedidosCommand;
using Application.Features.PedidosDet.Commands.InsertarPedidosCommand;
using Application.Features.PedidosDet.Queries.PedidoDetXId;
using Application.Features.PedidosDet.Queries.PedidosDetXParametros;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PedidoDetController : BaseApiController
    {
        private readonly ILogger<PedidoDetController> _logger;

        public PedidoDetController(ILogger<PedidoDetController> logger)
        {
            this._logger = logger;
        }
        /// <summary>
        ///  hola
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PedidoDetDTO>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<>))]
        public async Task<IActionResult> GetPedidoDetXParametros([FromQuery] PedidoDetParametros parametros)
        {
            this._logger.LogInformation("Entro al GET X PARAMETROS de PedidoDet");
            return Ok(await this.Mediator.Send(new PedidosDetXParametrosQuery
            {
                NumeroDePagina = parametros.NumeroDePagina,
                RegistrosXPagina = parametros.RegistrosXPagina,
                Estatus = parametros.Estatus,
                IdCliente = parametros.IdCliente
            }));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PedidoDetDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<>))]
        public async Task<IActionResult> GetXIdPedidoDet(int id)
        {
            this._logger.LogInformation("Entro al GET X Id de PedidoDet");
            return Ok(await this.Mediator.Send(new PedidoDetXIdQuery { Id = id }));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<int>))]
        public async Task<IActionResult> PostPedidoDet(InsertarPedidoDetCommand command)
        {
            this._logger.LogInformation("Entro al POST de PedidoDet");
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<int>))]
        public async Task<IActionResult> PutPedidoDet(int id, ActualizarPedidoDetCommand actualizar)
        {
            this._logger.LogInformation("Entro al PUT de PedidoDet");
            if (id != actualizar.Id)
                return BadRequest();

            return Ok(await this.Mediator.Send(actualizar));
        }


        /// <summary>
        /// Borrar una partida de Pedido
        /// </summary>
        /// <param name="id">Id del Pedido A Eliminar</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<int>))]
        public async Task<IActionResult> DeletePedidoDet(int id)
        {
            this._logger.LogInformation("Entro al DELETE de PedidoDet");
            return Ok(await this.Mediator.Send(new EliminarPedidoDetCommand { Id = id }));
        }
    }
}
