using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Interfaz del Repositorio de Persona
    /// </summary>
    public interface IPersonaRepositorio
    {
        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        Task<IEnumerable<PersonaEntidad>> ObtenerTodasAsync();

        /// <summary>
        /// Obtener la Persona por la Identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <returns>Informacion de la Persona consultada</returns>
        Task<PersonaEntidad> ObtenerPorIdentificacionAsync(string identificacion);

        /// <summary>
        /// Crear una persona nueva
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        Task NuevoAsync(PersonaEntidad personaEntidad);

        /// <summary>
        /// Actualizar los datos de una persona
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        Task ModificarAsync(PersonaEntidad personaEntidad);

        /// <summary>
        /// Eliminar una persona por la identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        Task EliminarAsync(string identificacion);
    }
}
