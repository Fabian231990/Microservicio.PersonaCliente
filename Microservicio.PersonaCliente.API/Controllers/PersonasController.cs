using Microservicio.PersonaCliente.Aplicacion.Interfaces;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.PersonaCliente.API.Controllers
{
    /// <summary>
    /// Controlador que maneja las operaciones CRUD para la entidad Persona.
    /// </summary>
    /// <param name="iPersonaServicio">Servicio que maneja la lógica de negocio para Persona.</param>
    [ApiController]
    [Route("[controller]")]
    public class PersonasController(IPersonaServicio iPersonaServicio) : ControllerBase
    {
        /// <summary>
        /// Servicio que maneja la lógica de negocio para Persona.
        /// </summary>
        private readonly IPersonaServicio iPersonaServicio = iPersonaServicio;

        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<PersonaEntidad>>>> ObtenerPersonas()
        {
            Respuesta<IEnumerable<PersonaEntidad>> resultado = await iPersonaServicio.ObtenerPersonasAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtener la Persona por la Identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <returns>Informacion de la Persona consultada</returns>
        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Respuesta<PersonaEntidad>>> ObtenerPersona(string identificacion)
        {
            Respuesta<PersonaEntidad> resultado = await iPersonaServicio.ObtenerPersonaAsync(identificacion);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Crear una persona nueva
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        [HttpPost]
        public async Task<ActionResult<Respuesta<PersonaEntidad>>> CrearPersona(PersonaEntidad personaEntidad)
        {
            Respuesta<PersonaEntidad> resultado = await iPersonaServicio.CrearPersonaAsync(personaEntidad);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return CreatedAtAction(nameof(ObtenerPersona), new { identificacion = personaEntidad.Identificacion }, resultado);
        }

        /// <summary>
        /// Actualizar los datos de una persona
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <param name="personaEntidad">Entidad Persona</param>
        [HttpPut("{identificacion}")]
        public async Task<ActionResult<Respuesta<PersonaEntidad>>> ActualizarPersona(string identificacion, PersonaEntidad personaEntidad)
        {
            Respuesta<PersonaEntidad> resultado = await iPersonaServicio.ActualizarPersonaAsync(identificacion, personaEntidad);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Eliminar una persona por la identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        [HttpDelete("{identificacion}")]
        public async Task<ActionResult<Respuesta<string>>> EliminarPersona(string identificacion)
        {
            Respuesta<string> resultado = await iPersonaServicio.EliminarPersonaAsync(identificacion);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }
    }
}
