using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.PersonaCliente.API.Controllers
{
    /// <summary>
    /// Controlador que maneja las operaciones CRUD para la entidad Cliente.
    /// </summary>
    /// <param name="iClienteServicio">Servicio que maneja la lógica de negocio para Cliente.</param>
    [ApiController]
    [Route("[controller]")]
    public class ClientesController(IClienteServicio iClienteServicio) : ControllerBase
    {
        private readonly IClienteServicio iClienteServicio = iClienteServicio;

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Una respuesta con una colección de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<ClienteEntidad>>>> ObtenerClientes()
        {
            Respuesta<IEnumerable<ClienteEntidad>> resultado = await iClienteServicio.ObtenerClientesAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene un cliente específico por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a buscar.</param>
        /// <returns>Una respuesta con los datos del cliente si se encuentra.</returns>
        [HttpGet("{idCliente}")]
        public async Task<ActionResult<Respuesta<ClienteEntidad>>> ObtenerClientePorId(int idCliente)
        {
            Respuesta<ClienteEntidad> resultado = await iClienteServicio.ObtenerClientePorIdAsync(idCliente);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad que se va a crear.</param>
        /// <returns>Una respuesta con el cliente creado.</returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<ClienteEntidad>>> CrearCliente(ClienteEntidad clienteEntidad)
        {
            Respuesta<ClienteEntidad> resultado = await iClienteServicio.CrearClienteAsync(clienteEntidad);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return CreatedAtAction(nameof(ObtenerClientePorId), new { idCliente = clienteEntidad.IdCliente }, resultado);
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a actualizar.</param>
        /// <param name="clienteEntidad">El objeto ClienteEntidad con los datos actualizados.</param>
        /// <returns>Una respuesta con el cliente actualizado.</returns>
        [HttpPut("{idCliente}")]
        public async Task<ActionResult<Respuesta<ClienteEntidad>>> ActualizarCliente(int idCliente, ClienteEntidad clienteEntidad)
        {
            Respuesta<ClienteEntidad> resultado = await iClienteServicio.ActualizarClienteAsync(idCliente, clienteEntidad);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a eliminar.</param>
        /// <returns>Una respuesta que indica el resultado de la operación.</returns>
        [HttpDelete("{idCliente}")]
        public async Task<ActionResult<Respuesta<string>>> EliminarCliente(int idCliente)
        {
            Respuesta<string> resultado = await iClienteServicio.EliminarClienteAsync(idCliente);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }
    }
}