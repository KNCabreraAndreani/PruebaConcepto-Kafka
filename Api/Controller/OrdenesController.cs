using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controller
{
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenesService _ordenesService;
        private readonly ILogger<OrdenesController> _logger;
        public OrdenesController(IOrdenesService ordenesService, ILogger<OrdenesController> logger)
        {
            _logger = logger;
            _ordenesService = ordenesService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageOrden message)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                await _ordenesService.GenerarMensajeKafka(message);
                return Ok("Mensaje procesado");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Hubo error al procesar mensaje", e.Message);
                return StatusCode(500);
            }

        }
    }
}
