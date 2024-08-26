using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Servicio para la gestion de clientes.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del servicio de clientes.
    /// </remarks>
    /// <param name="clienteRepositorio">Repositorio para la gestion de clientes.</param>
    /// <param name="personaRepositorio">Repositorio para la gestion de personas.</param>
    public class ClienteServicio(IClienteRepositorio clienteRepositorio, IPersonaRepositorio personaRepositorio) : IClienteServicio
    {
        /// <summary>
        /// Repositorio para la gestion de clientes
        /// </summary>
        private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio;

        /// <summary>
        /// Repositorio para la gestion de personas.
        /// </summary>
        private readonly IPersonaRepositorio _personaRepositorio = personaRepositorio;

        /// <summary>
        /// Obtiene toda la lista de clientes en formato DTO.
        /// </summary>
        /// <returns>Respuesta con la lista de clientes.</returns>
        public async Task<Respuesta<IEnumerable<ClienteDto>>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepositorio.ObtenerTodosAsync();
            if (!clientes.Any())
            {
                return Respuesta<IEnumerable<ClienteDto>>.CrearRespuestaFallida(404, "No se encontraron clientes registrados.");
            }

            return Respuesta<IEnumerable<ClienteDto>>.CrearRespuestaExitosa(clientes);
        }

        /// <summary>
        /// Obtiene un cliente por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el cliente encontrado.</returns>
        public async Task<Respuesta<ClienteDto>> ObtenerPorIdentificacionAsync(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(400, "La identificacion es obligatoria.");
            }

            var cliente = await _clienteRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (cliente == null)
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            return Respuesta<ClienteDto>.CrearRespuestaExitosa(cliente);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente a crear.</param>
        /// <returns>Respuesta con el cliente creado.</returns>
        public async Task<Respuesta<ClienteDto>> CrearAsync(ClienteDto clienteDto)
        {
            // Validaciones iniciales
            if (clienteDto == null || string.IsNullOrEmpty(clienteDto.Identificacion) || string.IsNullOrEmpty(clienteDto.Contrasenia))
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(400, "Datos del cliente invalidos.");
            }

            // Verificar si la persona existe
            var persona = await _personaRepositorio.ObtenerPorIdentificacionAsync(clienteDto.Identificacion);
            if (persona == null)
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(404, "Persona asociada no encontrada.");
            }

            // Verificar si ya existe un cliente para esta persona
            var clienteExistente = await _clienteRepositorio.ObtenerPorIdentificacionAsync(clienteDto.Identificacion);
            if (clienteExistente != null)
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(409, "Ya existe un cliente asociado con esta persona.");
            }

            // Crear el cliente
            ClienteDto clienteRespuestaDto = await _clienteRepositorio.NuevoAsync(clienteDto);
            return Respuesta<ClienteDto>.CrearRespuestaExitosa(clienteRespuestaDto);
        }

        /// <summary>
        /// Modifica los datos de un cliente existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <param name="clienteDto">DTO del cliente a modificar.</param>
        /// <returns>Respuesta con el cliente modificado.</returns>
        public async Task<Respuesta<ClienteDto>> ModificarAsync(string identificacion, ClienteDto clienteDto)
        {
            // Validaciones iniciales
            if (clienteDto == null || string.IsNullOrEmpty(clienteDto.Identificacion))
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(400, "Datos del cliente invalidos.");
            }

            if (identificacion != clienteDto.Identificacion)
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(400, "La identificacion proporcionada no coincide con la de la entidad.");
            }

            // Verificar si el cliente existe
            var clienteExistente = await _clienteRepositorio.ObtenerPorIdentificacionAsync(clienteDto.Identificacion);
            if (clienteExistente == null)
            {
                return Respuesta<ClienteDto>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            // Modificar el cliente
            ClienteDto clienteRespuestaDto = await _clienteRepositorio.ModificarAsync(clienteDto);
            return Respuesta<ClienteDto>.CrearRespuestaExitosa(clienteRespuestaDto);
        }

        /// <summary>
        /// Elimina un cliente por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        public async Task<Respuesta<string>> EliminarAsync(string identificacion)
        {
            // Validar que la identificacion no sea nula o vacia
            if (string.IsNullOrEmpty(identificacion))
            {
                return Respuesta<string>.CrearRespuestaFallida(400, "La identificacion es obligatoria.");
            }

            // Verificar si el cliente existe
            var cliente = await _clienteRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (cliente == null)
            {
                return Respuesta<string>.CrearRespuestaFallida(404, "Cliente no encontrado.");
            }

            // Eliminar el cliente
            await _clienteRepositorio.EliminarAsync(identificacion);
            return Respuesta<string>.CrearRespuestaExitosa("Cliente eliminado exitosamente.");
        }
    }
}
