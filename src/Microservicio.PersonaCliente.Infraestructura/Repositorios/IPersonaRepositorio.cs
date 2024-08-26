using Microservicio.PersonaCliente.Dominio.Dto;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad Persona, responsable de manejar las operaciones CRUD con la base de datos.
    /// </summary>
    public interface IPersonaRepositorio
    {
        /// <summary>
        /// Obtiene toda la lista de personas de la base de datos.
        /// </summary>
        /// <returns>Una tarea que representa una coleccion de entidades Persona.</returns>
        Task<IEnumerable<PersonaDto>> ObtenerTodasAsync();

        /// <summary>
        /// Obtiene una persona de la base de datos por su identificacion.
        /// </summary>
        /// <param name="identificacion">El numero de identificacion de la persona.</param>
        /// <returns>Una tarea que representa la entidad Persona correspondiente a la identificacion proporcionada.</returns>
        Task<PersonaDto> ObtenerPorIdentificacionAsync(string identificacion);

        /// <summary>
        /// Crea una nueva persona en la base de datos y retorna el registro creado.
        /// </summary>
        /// <param name="personaDto">DTO de la persona que se va a crear.</param>
        /// <returns>PersonaDto con la informacion de la persona creada.</returns>
        Task<PersonaDto> NuevoAsync(PersonaDto personaDto);

        /// <summary>
        /// Actualiza los datos de una persona existente en la base de datos.
        /// </summary>
        /// <param name="personaDto">DTO de la persona que se va a crear.</param>
        /// <returns>Una tarea que representa la operacion asincronica.</returns>
        Task ModificarAsync(PersonaDto personaDto);

        /// <summary>
        /// Elimina una persona de la base de datos por su identificacion.
        /// </summary>
        /// <param name="identificacion">El numero de identificacion de la persona que se va a eliminar.</param>
        /// <returns>Una tarea que representa la operacion asincronica.</returns>
        Task EliminarAsync(string identificacion);
    }
}
