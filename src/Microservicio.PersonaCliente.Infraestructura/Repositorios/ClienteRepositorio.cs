using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Repositorio de la entidad Cliente, implementa las operaciones CRUD utilizando ClienteDto.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del repositorio de Cliente con el contexto de base de datos proporcionado.
    /// </remarks>
    /// <param name="dbContext">Contexto de base de datos.</param>
    public class ClienteRepositorio(EjercicioTecnicoDBContext dbContext) : IClienteRepositorio
    {
        /// <summary>
        /// Contexto de base de datos.
        /// </summary>
        private readonly EjercicioTecnicoDBContext _dbContext = dbContext;

        /// <summary>
        /// Obtiene todos los clientes registrados en la base de datos en formato DTO.
        /// </summary>
        /// <returns>Una lista con todos los clientes registrados en formato DTO.</returns>
        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _dbContext.Clientes
                .Include(c => c.Persona)
                .ToListAsync();

            return clientes.Select(c => new ClienteDto
            {
                IdCliente = c.IdCliente,
                IdPersona = c.IdPersona,
                Identificacion = c.Persona.Identificacion,
                Contrasenia = c.Contrasenia,
                Estado = c.Estado
            }).ToList();
        }

        /// <summary>
        /// Obtiene un cliente por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        /// <returns>El cliente en formato DTO o null si no existe.</returns>
        public async Task<ClienteDto> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var cliente = await _dbContext.Clientes
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(c => c.Persona.Identificacion == identificacion);

            if (cliente == null) return null;

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                IdPersona = cliente.IdPersona,
                Identificacion = cliente.Persona.Identificacion,
                Contrasenia = cliente.Contrasenia,
                Estado = cliente.Estado
            };
        }

        /// <summary>
        /// Crea un nuevo cliente en la base de datos y retorna el registro creado.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente que se va a crear.</param>
        /// <returns>ClienteDto con la informacion del cliente creado.</returns>
        public async Task<ClienteDto> NuevoAsync(ClienteDto clienteDto)
        {
            var persona = await _dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == clienteDto.Identificacion);
            if (persona == null)
            {
                return null;
            }

            var clienteEntidad = new ClienteEntidad
            {
                IdPersona = persona.IdPersona,
                Contrasenia = clienteDto.Contrasenia,
                Estado = clienteDto.Estado
            };

            _dbContext.Clientes.Add(clienteEntidad);
            await _dbContext.SaveChangesAsync();

            return new ClienteDto
            {
                IdCliente = clienteEntidad.IdCliente,
                IdPersona = clienteEntidad.IdPersona,
                Identificacion = clienteDto.Identificacion,
                Contrasenia = clienteEntidad.Contrasenia,
                Estado = clienteEntidad.Estado
            };
        }


        /// <summary>
        /// Modifica los datos de un cliente existente en la base de datos y retorna el cliente actualizado.
        /// </summary>
        /// <param name="clienteDto">DTO del cliente con los datos actualizados.</param>
        /// <returns>ClienteDto con la informacion del cliente actualizado.</returns>
        public async Task<ClienteDto> ModificarAsync(ClienteDto clienteDto)
        {
            var cliente = await _dbContext.Clientes
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(c => c.Persona.Identificacion == clienteDto.Identificacion);

            if (cliente == null)
            {
                return null;
            }

            cliente.Contrasenia = clienteDto.Contrasenia;
            cliente.Estado = clienteDto.Estado;
            await _dbContext.SaveChangesAsync();

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                IdPersona = cliente.IdPersona,
                Identificacion = cliente.Persona.Identificacion,
                Contrasenia = cliente.Contrasenia,
                Estado = cliente.Estado
            };
        }


        /// <summary>
        /// Elimina un cliente de la base de datos por la identificacion de la persona asociada.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona asociada al cliente.</param>
        public async Task EliminarAsync(string identificacion)
        {
            var cliente = await _dbContext.Clientes
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(c => c.Persona.Identificacion == identificacion);

            if (cliente != null)
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
