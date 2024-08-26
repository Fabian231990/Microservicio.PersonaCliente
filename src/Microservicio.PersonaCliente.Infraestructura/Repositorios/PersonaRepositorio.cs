using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Infraestructura.Repositorios
{
    /// <summary>
    /// Repositorio de la entidad Persona, implementa las operaciones CRUD.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del repositorio de Persona con el contexto de base de datos proporcionado.
    /// </remarks>
    /// <param name="dbContext">Contexto de base de datos.</param>
    public class PersonaRepositorio(EjercicioTecnicoDBContext dbContext) : IPersonaRepositorio
    {
        /// <summary>
        /// Contexto de base de datos.
        /// </summary>
        private readonly EjercicioTecnicoDBContext _dbContext = dbContext;

        /// <summary>
        /// Obtiene todas las personas registradas en la base de datos.
        /// </summary>
        /// <returns>Una lista con todas las personas registradas en formato DTO.</returns>
        public async Task<IEnumerable<PersonaDto>> ObtenerTodasAsync()
        {
            var personas = await _dbContext.Personas.ToListAsync();
            return personas.Select(p => new PersonaDto
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                Genero = p.Genero,
                Edad = p.Edad,
                Identificacion = p.Identificacion,
                Direccion = p.Direccion,
                Telefono = p.Telefono
            }).ToList();
        }

        /// <summary>
        /// Obtiene una persona por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a buscar.</param>
        /// <returns>La persona encontrada en formato DTO o null si no existe.</returns>
        public async Task<PersonaDto> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var persona = await _dbContext.Personas.FirstOrDefaultAsync(f => f.Identificacion == identificacion);
            if (persona == null) return null;

            return new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Nombre = persona.Nombre,
                Genero = persona.Genero,
                Edad = persona.Edad,
                Identificacion = persona.Identificacion,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            };
        }

        /// <summary>
        /// Crea una nueva persona en la base de datos y retorna el registro creado.
        /// </summary>
        /// <param name="personaDto">DTO de la persona que se va a crear.</param>
        /// <returns>PersonaDto con la informacion de la persona creada.</returns>
        public async Task<PersonaDto> NuevoAsync(PersonaDto personaDto)
        {
            var personaEntidad = new PersonaEntidad
            {
                Nombre = personaDto.Nombre,
                Genero = personaDto.Genero,
                Edad = personaDto.Edad,
                Identificacion = personaDto.Identificacion,
                Direccion = personaDto.Direccion,
                Telefono = personaDto.Telefono
            };

            _dbContext.Personas.Add(personaEntidad);
            await _dbContext.SaveChangesAsync();

            return new PersonaDto
            {
                IdPersona = personaEntidad.IdPersona,
                Nombre = personaEntidad.Nombre,
                Genero = personaEntidad.Genero,
                Edad = personaEntidad.Edad,
                Identificacion = personaEntidad.Identificacion,
                Direccion = personaEntidad.Direccion,
                Telefono = personaEntidad.Telefono
            };
        }


        /// <summary>
        /// Modifica los datos de una persona existente en la base de datos.
        /// </summary>
        /// <param name="personaDto">DTO de la persona con los datos actualizados.</param>
        public async Task ModificarAsync(PersonaDto personaDto)
        {
            var persona = await _dbContext.Personas.FirstOrDefaultAsync(p => p.IdPersona == personaDto.IdPersona);

            if (persona != null)
            {
                persona.Nombre = personaDto.Nombre;
                persona.Genero = personaDto.Genero;
                persona.Edad = personaDto.Edad;
                persona.Direccion = personaDto.Direccion;
                persona.Telefono = personaDto.Telefono;

                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Elimina una persona de la base de datos por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona que se va a eliminar.</param>
        public async Task EliminarAsync(string identificacion)
        {
            var persona = await _dbContext.Personas.FirstOrDefaultAsync(f => f.Identificacion == identificacion);
            if (persona != null)
            {
                _dbContext.Personas.Remove(persona);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
