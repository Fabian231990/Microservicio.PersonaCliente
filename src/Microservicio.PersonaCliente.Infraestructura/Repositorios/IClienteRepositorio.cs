using Microservicio.PersonaCliente.Dominio.Dto;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    public interface IClienteRepositorio
    {
        /// <summary>
        /// Obtiene todos los clientes registrados en la base de datos en formato DTO.
        /// </summary>
        /// <returns>Una lista con todos los clientes registrados en formato DTO.</returns>
        Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene un cliente por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>El cliente en formato DTO o null si no existe.</returns>
        Task<ClienteDto> ObtenerPorIdentificacionAsync(string identificacion);

        /// <summary>
        /// Crea un nuevo cliente en la base de datos y retorna el registro creado.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente que se va a crear.</param>
        /// <returns>ClienteDto con la informacion del cliente creado.</returns>
        Task<ClienteDto> NuevoAsync(ClienteDto clienteDto);

        /// <summary>
        /// Modifica los datos de un cliente existente en la base de datos y retorna el cliente actualizado.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente con los datos actualizados.</param>
        /// <returns>ClienteDto con la informacion del cliente actualizado.</returns>
        Task<ClienteDto> ModificarAsync(ClienteDto clienteDto);

        /// <summary>
        /// Elimina un cliente de la base de datos por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        Task EliminarAsync(string identificacion);
    }
}
