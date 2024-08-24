using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Interfaces;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Repositorio Cliente
    /// </summary>
    /// <param name="ejercicioTecnicoDBContext">Clase Context de la Base de Datos</param>
    public class ClienteRepositorio(EjercicioTecnicoDBContext ejercicioTecnicoDBContext) : IClienteRepositorio
    {
        /// <summary>
        /// Clase Context de la Base de Datos
        /// </summary>
        private readonly EjercicioTecnicoDBContext ejercicioTecnicoDBContext = ejercicioTecnicoDBContext;

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Una colección enumerable de objetos ClienteEntidad.</returns>
        public async Task<IEnumerable<ClienteEntidad>> ObtenerTodosAsync()
        {
            return await ejercicioTecnicoDBContext.Cliente.ToListAsync();
        }

        /// <summary>
        /// Obtiene un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a buscar.</param>
        /// <returns>Un objeto ClienteEntidad si se encuentra; de lo contrario, null.</returns>
        public async Task<ClienteEntidad> ObtenerPorIdAsync(int idCliente)
        {
            return await ejercicioTecnicoDBContext.Cliente.FirstOrDefaultAsync(c => c.IdCliente == idCliente);
        }

        /// <summary>
        /// Obtiene un cliente basado en el ID de la persona asociada.
        /// </summary>
        /// <param name="idPersona">El ID de la persona que se está buscando.</param>
        /// <returns>Un objeto ClienteEntidad si se encuentra un cliente asociado a la persona; de lo contrario, null.</returns>
        public async Task<ClienteEntidad> ObtenerPorIdPersona(int idPersona)
        {
            return await ejercicioTecnicoDBContext.Cliente.FirstOrDefaultAsync(c => c.IdPersona == idPersona);
        }

        /// <summary>
        /// Agrega un nuevo cliente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad que se va a agregar.</param>
        public async Task NuevoAsync(ClienteEntidad clienteEntidad)
        {
            await ejercicioTecnicoDBContext.Cliente.AddAsync(clienteEntidad);
            await ejercicioTecnicoDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Modifica un cliente existente.
        /// </summary>
        /// <param name="clienteEntidad">El objeto ClienteEntidad con los nuevos datos.</param>
        public async Task ModificarAsync(ClienteEntidad clienteEntidad)
        {
            ClienteEntidad? cliente = await ejercicioTecnicoDBContext.Cliente.FirstOrDefaultAsync(p => p.IdCliente == clienteEntidad.IdCliente);

            if (cliente is not null)
            {
                cliente.Estado = clienteEntidad.Estado;
                cliente.Contrasenia = clienteEntidad.Contrasenia;

                await ejercicioTecnicoDBContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idCliente">El ID del cliente a eliminar.</param>
        public async Task EliminarAsync(int idCliente)
        {
            ClienteEntidad cliente = await ejercicioTecnicoDBContext.Cliente.FindAsync(idCliente);
            if (cliente is not null)
            {
                ejercicioTecnicoDBContext.Cliente.Remove(cliente);
                await ejercicioTecnicoDBContext.SaveChangesAsync();
            }
        }
    }
}
