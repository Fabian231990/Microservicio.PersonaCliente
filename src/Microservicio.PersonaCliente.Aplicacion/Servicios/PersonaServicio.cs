using Microservicio.PersonaCliente.Aplicacion.Interfaces;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Interfaces;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Contructor de la Clase
    /// </summary>
    /// <param name="personaRepositorio">Interfaz del Repositorio de Persona</param>
    public class PersonaServicio(IPersonaRepositorio iPersonaRepositorio) : IPersonaServicio
    {
        /// <summary>
        /// Interfaz del Repositorio de Persona
        /// </summary>
        private readonly IPersonaRepositorio iPersonaRepositorio = iPersonaRepositorio;

        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        public async Task<Respuesta<IEnumerable<PersonaEntidad>>> ObtenerPersonasAsync()
        {
            IEnumerable<PersonaEntidad> personas = await iPersonaRepositorio.ObtenerTodasAsync();
            return Respuesta<IEnumerable<PersonaEntidad>>.CrearRespuestaExitosa(personas);
        }

        /// <summary>
        /// Obtener la Persona por la Identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <returns>Informacion de la Persona consultada</returns>
        public async Task<Respuesta<PersonaEntidad>> ObtenerPersonaAsync(string identificacion)
        {
            PersonaEntidad persona = await iPersonaRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (persona == null)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(404, "La persona no fue encontrada.");
            }

            return Respuesta<PersonaEntidad>.CrearRespuestaExitosa(persona);
        }

        /// <summary>
        /// Crear una persona nueva
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        public async Task<Respuesta<PersonaEntidad>> CrearPersonaAsync(PersonaEntidad personaEntidad)
        {
            if (personaEntidad == null || string.IsNullOrEmpty(personaEntidad.Identificacion) || string.IsNullOrEmpty(personaEntidad.Nombre))
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(400, "Los campos 'Identificación' y 'Nombre' son obligatorios.");
            }

            PersonaEntidad personaExistente = await iPersonaRepositorio.ObtenerPorIdentificacionAsync(personaEntidad.Identificacion);
            if (personaExistente != null)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(409, "La identificación ya existe en la base de datos.");
            }

            try
            {
                await iPersonaRepositorio.NuevoAsync(personaEntidad);
                return Respuesta<PersonaEntidad>.CrearRespuestaExitosa(personaEntidad);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(2627, "La identificación ya existe en la base de datos.");
            }
            catch (Exception ex)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(500, $"Error inesperado: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualizar los datos de una persona
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <param name="personaEntidad">Entidad Persona</param>
        public async Task<Respuesta<PersonaEntidad>> ActualizarPersonaAsync(string identificacion, PersonaEntidad personaEntidad)
        {
            if (!identificacion.Equals(personaEntidad.Identificacion))
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(400, "La identificación proporcionada no coincide con la de la entidad.");
            }

            PersonaEntidad personaExistente = await iPersonaRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (personaExistente == null)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(404, "La persona no fue encontrada.");
            }

            try
            {
                await iPersonaRepositorio.ModificarAsync(personaEntidad);
                return Respuesta<PersonaEntidad>.CrearRespuestaExitosa(personaEntidad);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(409, "La operación no se pudo completar porque los datos fueron modificados o eliminados por otro proceso.");
            }
            catch (Exception ex)
            {
                return Respuesta<PersonaEntidad>.CrearRespuestaFallida(500, $"Error al actualizar la persona: {ex.Message}");
            }
        }

        /// <summary>
        /// Eliminar una persona por la identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        public async Task<Respuesta<string>> EliminarPersonaAsync(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return Respuesta<string>.CrearRespuestaFallida(400, "La identificación no puede estar vacía.");
            }

            PersonaEntidad personaExistente = await iPersonaRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (personaExistente == null)
            {
                return Respuesta<string>.CrearRespuestaFallida(404, $"No se encontró ninguna persona con la identificación {identificacion}.");
            }

            try
            {
                await iPersonaRepositorio.EliminarAsync(identificacion);
                return Respuesta<string>.CrearRespuestaExitosa($"La persona con identificación {identificacion} ha sido eliminada.");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return Respuesta<string>.CrearRespuestaFallida(409, "No se puede eliminar la persona porque está relacionada con otros registros.");
            }
            catch (Exception ex)
            {
                return Respuesta<string>.CrearRespuestaFallida(500, $"Error al eliminar la persona: {ex.Message}");
            }
        }
    }
}
