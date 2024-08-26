using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.PersonaCliente.API.Controllers
{
    /// <summary>
    /// Controlador para la gestion de clientes.
    /// </summary>
    /// <remarks>
    /// Constructor del controlador de clientes.
    /// </remarks>
    /// <param name="clienteServicio">Servicio para la gestion de clientes.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(IClienteServicio clienteServicio) : ControllerBase
    {
        /// <summary>
        /// Servicio para la gestion de clientes.
        /// </summary>
        private readonly IClienteServicio _clienteServicio = clienteServicio;

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Respuesta con la lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<ClienteDto>>>> ObtenerClientes()
        {
            var respuesta = await _clienteServicio.ObtenerTodosAsync();
            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene un cliente por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el cliente encontrado.</returns>
        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Respuesta<ClienteDto>>> ObtenerCliente(string identificacion)
        {
            var respuesta = await _clienteServicio.ObtenerPorIdentificacionAsync(identificacion);
            if (!respuesta.EsExitoso)
            {
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente a crear.</param>
        /// <returns>Respuesta con el cliente creado.</returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<ClienteDto>>> CrearCliente(ClienteDto clienteDto)
        {
            var respuesta = await _clienteServicio.CrearAsync(clienteDto);
            if (!respuesta.EsExitoso)
            {
                return BadRequest(respuesta);
            }
            return CreatedAtAction(nameof(ObtenerCliente), new { identificacion = clienteDto.Identificacion }, respuesta);
        }

        /// <summary>
        /// Modifica un cliente existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <param name="clienteDto">DTO del cliente a modificar.</param>
        /// <returns>Respuesta con el cliente modificado.</returns>
        [HttpPut("{identificacion}")]
        public async Task<IActionResult> ModificarCliente(string identificacion, ClienteDto clienteDto)
        {
            var respuesta = await _clienteServicio.ModificarAsync(identificacion, clienteDto);
            if (!respuesta.EsExitoso)
            {
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Elimina un cliente por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        [HttpDelete("{identificacion}")]
        public async Task<IActionResult> EliminarCliente(string identificacion)
        {
            var respuesta = await _clienteServicio.EliminarAsync(identificacion);
            if (!respuesta.EsExitoso)
            {
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }
    }
}
