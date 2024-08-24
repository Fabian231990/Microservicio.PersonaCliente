using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de Cliente, que define las operaciones de negocio para la entidad Cliente.
    /// </summary>
    public interface IClienteServicio
    {
        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Una respuesta con una colección enumerable de objetos ClienteEntidad.</returns>
        Task<Respuesta<IEnumerable<ClienteEntidad>>> ObtenerClientesAsync();

        /// <summary>
        /// Obtiene un cliente específico por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a buscar.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad correspondiente al ID proporcionado.</returns>
        Task<Respuesta<ClienteEntidad>> ObtenerClientePorIdAsync(int idCliente);

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad que se va a crear.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad creado.</returns>
        Task<Respuesta<ClienteEntidad>> CrearClienteAsync(ClienteEntidad clienteEntidad);

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a actualizar.</param>
        /// <param name="clienteEntidad">El objeto ClienteEntidad con los datos actualizados.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad actualizado.</returns>
        Task<Respuesta<ClienteEntidad>> ActualizarClienteAsync(int idCliente, ClienteEntidad clienteEntidad);

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a eliminar.</param>
        /// <returns>Una respuesta que indica el resultado de la operación.</returns>
        Task<Respuesta<string>> EliminarClienteAsync(int idCliente);
    }
}
