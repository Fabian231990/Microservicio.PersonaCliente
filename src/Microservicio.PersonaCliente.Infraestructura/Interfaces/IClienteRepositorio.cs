using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Infraestructura.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de Cliente, que define las operaciones CRUD.
    /// </summary>
    public interface IClienteRepositorio
    {
        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Una colección enumerable de objetos ClienteEntidad.</returns>
        Task<IEnumerable<ClienteEntidad>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a buscar.</param>
        /// <returns>Un objeto ClienteEntidad si se encuentra; de lo contrario, null.</returns>
        Task<ClienteEntidad> ObtenerPorIdAsync(int idCliente);

        /// <summary>
        /// Obtiene un cliente basado en el ID de la persona asociada.
        /// </summary>
        /// <param name="idPersona">El ID de la persona que se está buscando.</param>
        /// <returns>Un objeto ClienteEntidad si se encuentra un cliente asociado a la persona; de lo contrario, null.</returns>
        Task<ClienteEntidad> ObtenerPorIdPersona(int idPersona);

        /// <summary>
        /// Agrega un nuevo cliente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad que se va a agregar.</param>
        Task NuevoAsync(ClienteEntidad clienteEntidad);

        /// <summary>
        /// Modifica un cliente existente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad con los nuevos datos.</param>
        Task ModificarAsync(ClienteEntidad clienteEntidad);

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a eliminar.</param>
        Task EliminarAsync(int idCliente);
    }
}
