using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Interfaces
{
    public interface IPersonaServicio
    {
        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        Task<Respuesta<IEnumerable<PersonaEntidad>>> ObtenerPersonas();

        /// <summary>
        /// Obtener la Persona por la Identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <returns>Informacion de la Persona consultada</returns>
        Task<Respuesta<PersonaEntidad>> ObtenerPersona(string identificacion);

        /// <summary>
        /// Crear una persona nueva
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        Task<Respuesta<PersonaEntidad>> CrearPersona(PersonaEntidad personaEntidad);

        /// <summary>
        /// Actualizar los datos de una persona
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <param name="personaEntidad">Entidad Persona</param>
        Task<Respuesta<PersonaEntidad>> ActualizarPersona(string identificacion, PersonaEntidad personaEntidad);

        /// <summary>
        /// Eliminar una persona por la identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        Task<Respuesta<string>> EliminarPersona(string identificacion);
    }
}
