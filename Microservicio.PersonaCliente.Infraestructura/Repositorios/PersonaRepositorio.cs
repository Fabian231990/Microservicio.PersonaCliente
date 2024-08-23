using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Repositorio Persona
    /// </summary>
    public class PersonaRepositorio : IPersonaRepositorio
    {
        /// <summary>
        /// Clase Context de la Base de Datos
        /// </summary>
        private readonly EjercicioTecnicoDBContext ejercicioTecnicoDBContext;

        /// <summary>
        /// Constructo de la Clase
        /// </summary>
        /// <param name="ejercicioTecnicoDBContext">Clase Context de la Base de Datos</param>
        public PersonaRepositorio(EjercicioTecnicoDBContext ejercicioTecnicoDBContext)
        {
            this.ejercicioTecnicoDBContext = ejercicioTecnicoDBContext;
        }

        /// <summary>
        /// Obtener toda la lista de Personas
        /// </summary>
        /// <returns>Listado con todas las personas registradas</returns>
        public async Task<IEnumerable<PersonaEntidad>> ObtenerTodas()
        {
            return await ejercicioTecnicoDBContext.Persona.ToListAsync();
        }

        /// <summary>
        /// Obtener la Persona por la Identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        /// <returns>Informacion de la Persona consultada</returns>
        public async Task<PersonaEntidad> ObtenerPorIdentificacion(string identificacion)
        {
            return await ejercicioTecnicoDBContext.Persona.FirstOrDefaultAsync(filtro => filtro.Identificacion == identificacion);
        }

        /// <summary>
        /// Crear una persona nueva
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        public async Task Nuevo(PersonaEntidad personaEntidad)
        {
            ejercicioTecnicoDBContext.Persona.Add(personaEntidad);
            await ejercicioTecnicoDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Actualizar los datos de una persona
        /// </summary>
        /// <param name="personaEntidad">Entidad Persona</param>
        public async Task Modificar(PersonaEntidad personaEntidad)
        {
            PersonaEntidad? persona = await ejercicioTecnicoDBContext.Persona
                .FirstOrDefaultAsync(p => p.IdPersona == personaEntidad.IdPersona);

            if (persona != null)
            {
                // Actualizar la entidad existente en lugar de agregar una nueva instancia
                persona.Nombre = personaEntidad.Nombre;
                persona.Genero = personaEntidad.Genero;
                persona.Edad = personaEntidad.Edad;
                persona.Direccion = personaEntidad.Direccion;
                persona.Telefono = personaEntidad.Telefono;

                await ejercicioTecnicoDBContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Eliminar una persona por la identificacion
        /// </summary>
        /// <param name="identificacion">Identificacion de la Persona</param>
        public async Task Eliminar(string identificacion)
        {
            PersonaEntidad? persona = await ejercicioTecnicoDBContext.Persona.FirstOrDefaultAsync(filtro => filtro.Identificacion == identificacion);
            if (persona != null)
            {
                ejercicioTecnicoDBContext.Persona.Remove(persona);
                await ejercicioTecnicoDBContext.SaveChangesAsync();
            }
        }
    }
}
