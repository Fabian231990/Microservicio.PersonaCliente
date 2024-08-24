using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Tests.Integracion
{
    public class PersonaIntegrationTests
    {
        private EjercicioTecnicoDBContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EjercicioTecnicoDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new EjercicioTecnicoDBContext(options);
        }

        [Fact]
        public async Task CrearPersona_DeberiaGuardarEnLaBaseDeDatos()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var nuevaPersona = new PersonaEntidad
            {
                Nombre = "Juan Perez",
                Genero = "Masculino",
                Edad = 30,
                Identificacion = "1234567890",
                Direccion = "Quito, Ecuador",
                Telefono = "0987654321"
            };

            await personaRepositorio.NuevoAsync(nuevaPersona);

            var personaGuardada = await dbContext.Persona.FirstOrDefaultAsync(p => p.Identificacion == "1234567890");
            Assert.NotNull(personaGuardada);
            Assert.Equal("Juan Perez", personaGuardada.Nombre);
        }

        [Fact]
        public async Task ObtenerPersona_DeberiaRetornarPersonaExistente()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaEntidad
            {
                Nombre = "Maria Lopez",
                Genero = "Femenino",
                Edad = 25,
                Identificacion = "0987654321",
                Direccion = "Guayaquil, Ecuador",
                Telefono = "0981234567"
            };

            await dbContext.Persona.AddAsync(persona);
            await dbContext.SaveChangesAsync();

            var personaObtenida = await personaRepositorio.ObtenerPorIdentificacionAsync("0987654321");

            Assert.NotNull(personaObtenida);
            Assert.Equal("Maria Lopez", personaObtenida.Nombre);
        }

        [Fact]
        public async Task ActualizarPersona_DeberiaModificarDatosExistentes()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaEntidad
            {
                Nombre = "Carlos Andrade",
                Genero = "Masculino",
                Edad = 40,
                Identificacion = "1122334455",
                Direccion = "Cuenca, Ecuador",
                Telefono = "0998765432"
            };

            await dbContext.Persona.AddAsync(persona);
            await dbContext.SaveChangesAsync();

            persona.Nombre = "Carlos Alberto Andrade";
            persona.Edad = 41;
            await personaRepositorio.ModificarAsync(persona);

            var personaActualizada = await dbContext.Persona.FirstOrDefaultAsync(p => p.Identificacion == "1122334455");
            Assert.NotNull(personaActualizada);
            Assert.Equal("Carlos Alberto Andrade", personaActualizada.Nombre);
            Assert.Equal(41, personaActualizada.Edad);
        }

        [Fact]
        public async Task EliminarPersona_DeberiaEliminarDeLaBaseDeDatos()
        {
            var dbContext = GetInMemoryDbContext();
            var personaRepositorio = new PersonaRepositorio(dbContext);

            var persona = new PersonaEntidad
            {
                Nombre = "Lucia Velez",
                Genero = "Femenino",
                Edad = 32,
                Identificacion = "2233445566",
                Direccion = "Loja, Ecuador",
                Telefono = "0981122334"
            };

            await dbContext.Persona.AddAsync(persona);
            await dbContext.SaveChangesAsync();

            await personaRepositorio.EliminarAsync(persona.Identificacion);

            var personaEliminada = await dbContext.Persona.FirstOrDefaultAsync(p => p.Identificacion == "2233445566");
            Assert.Null(personaEliminada);
        }
    }
}