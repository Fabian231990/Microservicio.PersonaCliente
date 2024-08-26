using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Tests.Integracion
{
    public class PersonaIntegrationTests
    {
        /// <summary>
        /// Configura el contexto de base de datos en memoria para las pruebas.
        /// </summary>
        /// <returns>Instancia del contexto de base de datos en memoria.</returns>
        private EjercicioTecnicoDBContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EjercicioTecnicoDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new EjercicioTecnicoDBContext(options);
        }

        /// <summary>
        /// Prueba para verificar que se puede crear una persona y guardarla en la base de datos.
        /// </summary>
        [Fact]
        public async Task CrearPersona_DeberiaGuardarEnLaBaseDeDatos()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var nuevaPersona = new PersonaDto
            {
                Nombre = "Juan Perez",
                Genero = "Masculino",
                Edad = 30,
                Identificacion = "1234567890",
                Direccion = "Quito, Ecuador",
                Telefono = "0987654321"
            };

            await personaRepositorio.NuevoAsync(nuevaPersona);

            var personaGuardada = await dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == "1234567890");
            Assert.NotNull(personaGuardada);
            Assert.Equal("Juan Perez", personaGuardada.Nombre);
        }

        /// <summary>
        /// Prueba para verificar que se puede obtener una persona existente en la base de datos.
        /// </summary>
        [Fact]
        public async Task ObtenerPersona_DeberiaRetornarPersonaExistente()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaDto
            {
                Nombre = "Maria Lopez",
                Genero = "Femenino",
                Edad = 25,
                Identificacion = "0987654321",
                Direccion = "Guayaquil, Ecuador",
                Telefono = "0981234567"
            };

            await dbContext.Personas.AddAsync(new PersonaEntidad
            {
                Nombre = persona.Nombre,
                Genero = persona.Genero,
                Edad = persona.Edad,
                Identificacion = persona.Identificacion,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            });
            await dbContext.SaveChangesAsync();

            var personaObtenida = await personaRepositorio.ObtenerPorIdentificacionAsync("0987654321");

            Assert.NotNull(personaObtenida);
            Assert.Equal("Maria Lopez", personaObtenida.Nombre);
        }

        /// <summary>
        /// Prueba para verificar que se puede actualizar una persona existente en la base de datos.
        /// </summary>
        [Fact]
        public async Task ActualizarPersona_DeberiaModificarDatosExistentes()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaDto
            {
                Nombre = "Carlos Andrade",
                Genero = "Masculino",
                Edad = 40,
                Identificacion = "1122334455",
                Direccion = "Cuenca, Ecuador",
                Telefono = "0998765432"
            };

            await dbContext.Personas.AddAsync(new PersonaEntidad
            {
                Nombre = persona.Nombre,
                Genero = persona.Genero,
                Edad = persona.Edad,
                Identificacion = persona.Identificacion,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            });
            await dbContext.SaveChangesAsync();

            persona.Nombre = "Carlos Alberto Andrade";
            persona.Edad = 41;
            await personaRepositorio.ModificarAsync(persona);

            var personaActualizada = await dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == "1122334455");
            Assert.NotNull(personaActualizada);
            Assert.Equal("Carlos Alberto Andrade", personaActualizada.Nombre);
            Assert.Equal(41, personaActualizada.Edad);
        }

        /// <summary>
        /// Prueba para verificar que se puede eliminar una persona de la base de datos.
        /// </summary>
        [Fact]
        public async Task EliminarPersona_DeberiaEliminarDeLaBaseDeDatos()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaDto
            {
                Nombre = "Lucia Velez",
                Genero = "Femenino",
                Edad = 32,
                Identificacion = "2233445566",
                Direccion = "Loja, Ecuador",
                Telefono = "0981122334"
            };

            await dbContext.Personas.AddAsync(new PersonaEntidad
            {
                Nombre = persona.Nombre,
                Genero = persona.Genero,
                Edad = persona.Edad,
                Identificacion = persona.Identificacion,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            });
            await dbContext.SaveChangesAsync();

            await personaRepositorio.EliminarAsync(persona.Identificacion);

            var personaEliminada = await dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == "2233445566");
            Assert.Null(personaEliminada);
        }
    }
}
