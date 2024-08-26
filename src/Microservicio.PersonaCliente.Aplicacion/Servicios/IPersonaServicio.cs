using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Interfaz para el servicio de gestion de personas.
    /// </summary>
    public interface IPersonaServicio
    {
        /// <summary>
        /// Obtiene toda la lista de personas en formato DTO.
        /// </summary>
        /// <returns>Respuesta con la lista de personas.</returns>
        Task<Respuesta<IEnumerable<PersonaDto>>> ObtenerTodasAsync();

        /// <summary>
        /// Obtiene una persona por su identificacion en formato DTO.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona.</param>
        /// <returns>Respuesta con la persona encontrada.</returns>
        Task<Respuesta<PersonaDto>> ObtenerPorIdentificacionAsync(string identificacion);

        /// <summary>
        /// Crea una nueva persona.
        /// </summary>
        /// <param name="personaDto">DTO de la persona a crear.</param>
        /// <returns>Respuesta con la persona creada.</returns>
        Task<Respuesta<PersonaDto>> CrearAsync(PersonaDto personaDto);

        /// <summary>
        /// Actualiza una persona existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a modificar.</param>
        /// <param name="personaDto">DTO de la persona a actualizar.</param>
        /// <returns>Respuesta con la persona actualizada.</returns>
        Task<Respuesta<PersonaDto>> ModificarAsync(string identificacion, PersonaDto personaDto);

        /// <summary>
        /// Elimina una persona por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        Task<Respuesta<string>> EliminarAsync(string identificacion);
    }
}
