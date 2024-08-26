using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Interfaz para el servicio de clientes, define las operaciones para la gestion de clientes.
    /// </summary>
    public interface IClienteServicio
    {
        /// <summary>
        /// Obtiene toda la lista de clientes en formato DTO.
        /// </summary>
        /// <returns>Respuesta con la lista de clientes.</returns>
        Task<Respuesta<IEnumerable<ClienteDto>>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene un cliente por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el cliente encontrado.</returns>
        Task<Respuesta<ClienteDto>> ObtenerPorIdentificacionAsync(string identificacion);

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente a crear.</param>
        /// <returns>Respuesta con el cliente creado.</returns>
        Task<Respuesta<ClienteDto>> CrearAsync(ClienteDto clienteDto);

        /// <summary>
        /// Modifica los datos de un cliente existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <param name="clienteDto">DTO del cliente a modificar.</param>
        /// <returns>Respuesta con el cliente modificado.</returns>
        Task<Respuesta<ClienteDto>> ModificarAsync(string identificacion, ClienteDto clienteDto);

        /// <summary>
        /// Elimina un cliente por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        Task<Respuesta<string>> EliminarAsync(string identificacion);
    }
}
