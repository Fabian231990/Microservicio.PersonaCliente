using Microservicio.PersonaCliente.Aplicacion.Interfaces;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.PersonaCliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        /// <summary>
        /// Interfaz del Servicio de Persona
        /// </summary>
        private readonly IPersonaServicio iPersonaServicio;

        /// <summary>
        /// Controlador de la Clase
        /// </summary>
        /// <param name="iPersonaServicio">Interfaz del Servicio de Persona</param>
        public PersonaController(IPersonaServicio iPersonaServicio)
        {
            this.iPersonaServicio = iPersonaServicio;
        }

        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<PersonaEntidad>>>> ObtenerPersonas()
        {
            var resultado = await iPersonaServicio.ObtenerPersonas();
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
            var resultado = await iPersonaServicio.ObtenerPersona(identificacion);
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
            var resultado = await iPersonaServicio.CrearPersona(personaEntidad);
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
            var resultado = await iPersonaServicio.ActualizarPersona(identificacion, personaEntidad);
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
            var resultado = await iPersonaServicio.EliminarPersona(identificacion);
            if (!resultado.EsExitoso)
            {
                return StatusCode(resultado.Codigo, resultado);
            }

            return Ok(resultado);
        }
    }
}
