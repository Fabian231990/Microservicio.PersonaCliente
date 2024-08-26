using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microservicio.PersonaCliente.Infraestructura.Utilitarios;

namespace Microservicio.PersonaCliente.Aplicacion.Servicios
{
    /// <summary>
    /// Servicio para la gestion de Personas.
    /// </summary>
    /// <remarks>
    /// Constructor del servicio de personas.
    /// </remarks>
    /// <param name="personaRepositorio">Interfaz para el repositorio de la entidad Persona.</param>
    public class PersonaServicio(IPersonaRepositorio personaRepositorio) : IPersonaServicio
    {
        /// <summary>
        /// Interfaz para el repositorio de la entidad Persona.
        /// </summary>
        private readonly IPersonaRepositorio _personaRepositorio = personaRepositorio;

        /// <summary>
        /// Obtiene toda la lista de personas en formato DTO.
        /// </summary>
        /// <returns>Respuesta con la lista de personas.</returns>
        public async Task<Respuesta<IEnumerable<PersonaDto>>> ObtenerTodasAsync()
        {
            var personas = await _personaRepositorio.ObtenerTodasAsync();
            if (!personas.Any())
            {
                return Respuesta<IEnumerable<PersonaDto>>.CrearRespuestaFallida(404, "No se encontraron personas registradas.");
            }

            return Respuesta<IEnumerable<PersonaDto>>.CrearRespuestaExitosa(personas);
        }

        /// <summary>
        /// Obtiene una persona por su identificacion en formato DTO.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona.</param>
        /// <returns>Respuesta con la persona encontrada.</returns>
        public async Task<Respuesta<PersonaDto>> ObtenerPorIdentificacionAsync(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(400, "La identificacion no puede estar vacia.");
            }

            var persona = await _personaRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (persona == null)
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(404, "Persona no encontrada.");
            }

            return Respuesta<PersonaDto>.CrearRespuestaExitosa(persona);
        }

        /// <summary>
        /// Crea una nueva persona.
        /// </summary>
        /// <param name="personaDto">DTO de la persona a crear.</param>
        /// <returns>Respuesta con la persona creada.</returns>
        public async Task<Respuesta<PersonaDto>> CrearAsync(PersonaDto personaDto)
        {
            // Validar que los campos obligatorios esten completos
            if (string.IsNullOrEmpty(personaDto.Identificacion) || string.IsNullOrEmpty(personaDto.Nombre))
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(400, "Los campos 'Identificacion' y 'Nombre' son obligatorios.");
            }

            // Verificar si ya existe una persona con la misma identificacion
            var personaExistente = await _personaRepositorio.ObtenerPorIdentificacionAsync(personaDto.Identificacion);
            if (personaExistente != null)
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(409, "Ya existe una persona con la misma identificacion.");
            }

            PersonaDto personaRespuestaDto = await _personaRepositorio.NuevoAsync(personaDto);
            return Respuesta<PersonaDto>.CrearRespuestaExitosa(personaRespuestaDto);
        }

        /// <summary>
        /// Actualiza una persona existente.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a modificar.</param>
        /// <param name="personaDto">DTO de la persona a actualizar.</param>
        /// <returns>Respuesta con la persona actualizada.</returns>
        public async Task<Respuesta<PersonaDto>> ModificarAsync(string identificacion, PersonaDto personaDto)
        {
            // Validar que los campos obligatorios esten completos
            if (string.IsNullOrEmpty(personaDto.Identificacion) || personaDto.IdPersona <= 0)
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(400, "Los campos 'Identificacion' y 'IdPersona' son obligatorios.");
            }

            if (identificacion != personaDto.Identificacion)
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(400, "La identificacion proporcionada no coincide con la de la entidad.");
            }

            // Verificar si la persona existe antes de intentar modificarla
            var personaExistente = await _personaRepositorio.ObtenerPorIdentificacionAsync(personaDto.Identificacion);
            if (personaExistente == null)
            {
                return Respuesta<PersonaDto>.CrearRespuestaFallida(404, "Persona no encontrada.");
            }

            // Permitir la modificacion solo de los campos Edad, Genero, Direccion y Telefono
            personaExistente.Edad = personaDto.Edad;
            personaExistente.Genero = personaDto.Genero;
            personaExistente.Direccion = personaDto.Direccion;
            personaExistente.Telefono = personaDto.Telefono;

            await _personaRepositorio.ModificarAsync(personaExistente);

            // Crear DTO actualizado para la respuesta
            var personaActualizadaDto = new PersonaDto
            {
                IdPersona = personaExistente.IdPersona,
                Nombre = personaExistente.Nombre,
                Genero = personaExistente.Genero,
                Edad = personaExistente.Edad,
                Identificacion = personaExistente.Identificacion,
                Direccion = personaExistente.Direccion,
                Telefono = personaExistente.Telefono
            };

            return Respuesta<PersonaDto>.CrearRespuestaExitosa(personaActualizadaDto);
        }


        /// <summary>
        /// Elimina una persona por su identificacion.
        /// </summary>
        /// <param name="identificacion">Identificacion de la persona a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminacion.</returns>
        public async Task<Respuesta<string>> EliminarAsync(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return Respuesta<string>.CrearRespuestaFallida(400, "La identificacion no puede estar vacia.");
            }

            // Verificar si la persona existe antes de intentar eliminarla
            var personaExistente = await _personaRepositorio.ObtenerPorIdentificacionAsync(identificacion);
            if (personaExistente == null)
            {
                return Respuesta<string>.CrearRespuestaFallida(404, "Persona no encontrada.");
            }

            await _personaRepositorio.EliminarAsync(identificacion);
            return Respuesta<string>.CrearRespuestaExitosa("Persona eliminada exitosamente.");
        }
    }
}
