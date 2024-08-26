using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Contructor de la Clase
    /// </summary>
    /// <param name="iClienteRepositorio">Interfaz del Repositorio de Cliente</param>
    public class ClienteServicio(IClienteRepositorio iClienteRepositorio) : IClienteServicio
    {
        private readonly IClienteRepositorio iClienteRepositorio = iClienteRepositorio;

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Una respuesta con una colección enumerable de objetos ClienteEntidad.</returns>
        public async Task<Respuesta<IEnumerable<ClienteEntidad>>> ObtenerClientesAsync()
        {
            IEnumerable<ClienteEntidad> clientes = await iClienteRepositorio.ObtenerTodosAsync();
            return Respuesta<IEnumerable<ClienteEntidad>>.CrearRespuestaExitosa(clientes);
        }

        /// <summary>
        /// Obtiene un cliente específico por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a buscar.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad correspondiente al ID proporcionado.</returns>
        public async Task<Respuesta<ClienteEntidad>> ObtenerClientePorIdAsync(int idCliente)
        {
            ClienteEntidad cliente = await iClienteRepositorio.ObtenerPorIdAsync(idCliente);
            if (cliente == null)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            return Respuesta<ClienteEntidad>.CrearRespuestaExitosa(cliente);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad que se va a crear.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad creado.</returns>
        public async Task<Respuesta<ClienteEntidad>> CrearClienteAsync(ClienteEntidad clienteEntidad)
        {
            if (clienteEntidad == null || clienteEntidad.IdPersona <= 0 || string.IsNullOrEmpty(clienteEntidad.Contrasenia))
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(400, "Datos del cliente inválidos.");
            }

            // Verificar si ya existe un cliente con el mismo IdPersona
            var clienteExistente = await iClienteRepositorio.ObtenerPorIdPersona(clienteEntidad.IdPersona);
            if (clienteExistente != null)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(409, "Ya existe un cliente asociado con esta persona.");
            }

            try
            {
                await iClienteRepositorio.NuevoAsync(clienteEntidad);
                return Respuesta<ClienteEntidad>.CrearRespuestaExitosa(clienteEntidad);
            }
            catch (Exception ex)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(500, $"Error al crear el cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a actualizar.</param>
        /// <param name="clienteEntidad">El objeto ClienteEntidad con los datos actualizados.</param>
        /// <returns>Una respuesta con el objeto ClienteEntidad actualizado.</returns>
        public async Task<Respuesta<ClienteEntidad>> ActualizarClienteAsync(int idCliente, ClienteEntidad clienteEntidad)
        {
            if (idCliente != clienteEntidad.IdCliente)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(400, "El ID del cliente proporcionado no coincide.");
            }

            ClienteEntidad clienteExistente = await iClienteRepositorio.ObtenerPorIdAsync(idCliente);
            if (clienteExistente == null)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            try
            {
                await iClienteRepositorio.ModificarAsync(clienteEntidad);
                return Respuesta<ClienteEntidad>.CrearRespuestaExitosa(clienteEntidad);
            }
            catch (Exception ex)
            {
                return Respuesta<ClienteEntidad>.CrearRespuestaFallida(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a eliminar.</param>
        /// <returns>Una respuesta que indica el resultado de la operación.</returns>
        public async Task<Respuesta<string>> EliminarClienteAsync(int idCliente)
        {
            ClienteEntidad clienteExistente = await iClienteRepositorio.ObtenerPorIdAsync(idCliente);
            if (clienteExistente == null)
            {
                return Respuesta<string>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            try
            {
                await iClienteRepositorio.EliminarAsync(idCliente);
                return Respuesta<string>.CrearRespuestaExitosa("Cliente eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Respuesta<string>.CrearRespuestaFallida(500, $"Error al eliminar el cliente: {ex.Message}");
            }
        }
    }
}
