using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.PersonaCliente.API.Controllers
{
    /// <summary>
    /// Controlador que maneja las operaciones relacionadas con las personas.
    /// </summary>
    /// <remarks>
    /// Constructor del controlador Persona.
    /// </remarks>
    /// <param name="personaServicio">Servicio de personas que maneja la logica de negocio.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController(IPersonaServicio personaServicio) : ControllerBase
    {
        /// <summary>
        /// Servicio de personas que maneja la logica de negocio.
        /// </summary>
        private readonly IPersonaServicio _personaServicio = personaServicio;

        /// <summary>
        /// Obtiene todas las personas registradas.
        /// </summary>
        /// <returns>Lista de personas.</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<PersonaDto>>>> ObtenerPersonas()
        {
            var respuesta = await _personaServicio.ObtenerTodasAsync();
            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene una persona por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a buscar.</param>
        /// <returns>Informacion de la persona encontrada.</returns>
        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Respuesta<PersonaDto>>> ObtenerPersona(string identificacion)
        {
            var respuesta = await _personaServicio.ObtenerPorIdentificacionAsync(identificacion);
            if (!respuesta.EsExitoso)
            {
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Crea una nueva persona.
        /// </summary>
        /// <param name="personaDto">Datos de la persona a crear.</param>
        /// <returns>Respuesta con la persona creada.</returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<PersonaDto>>> CrearPersona(PersonaDto personaDto)
        {
            var respuesta = await _personaServicio.CrearAsync(personaDto);
            return Ok(respuesta);
        }

        /// <summary>
        /// Modifica los datos de una persona existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a modificar.</param>
        /// <param name="personaDto">Datos actualizados de la persona.</param>
        /// <returns>Respuesta con la persona modificada.</returns>
        [HttpPut("{identificacion}")]
        public async Task<IActionResult> ModificarPersona(string identificacion, PersonaDto personaDto)
        {
            var respuesta = await _personaServicio.ModificarAsync(identificacion, personaDto);
            return Ok(respuesta);
        }

        /// <summary>
        /// Elimina una persona por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        [HttpDelete("{identificacion}")]
        public async Task<IActionResult> EliminarPersona(string identificacion)
        {
            var respuesta = await _personaServicio.EliminarAsync(identificacion);
            return Ok(respuesta);
        }
    }
}
