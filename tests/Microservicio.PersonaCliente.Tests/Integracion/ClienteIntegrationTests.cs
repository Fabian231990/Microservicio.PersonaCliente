using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Dominio.Dto;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Tests.Integracion
{
    public class ClienteIntegrationTests
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
        /// Prueba para verificar que se puede crear un cliente y guardarlo en la base de datos.
        /// </summary>
        [Fact]
        public async Task CrearCliente_DeberiaGuardarEnLaBaseDeDatos()
        {
            // Arrange: Configuramos el contexto en memoria y los repositorios
            var dbContext = GetInMemoryDbContext();
            var clienteRepositorio = new ClienteRepositorio(dbContext);
            var personaRepositorio = new PersonaRepositorio(dbContext);
            var clienteServicio = new ClienteServicio(clienteRepositorio, personaRepositorio);

            // Creamos un nuevo ClienteDto
            var nuevoCliente = new ClienteDto
            {
                Identificacion = "1234567890",
                Contrasenia = "MiContraseñaSegura",
                Estado = true
            };

            // Act: Ejecutamos el metodo CrearAsync en el servicio
            var resultado = await clienteServicio.CrearAsync(nuevoCliente);

            // Assert: Verificamos que el cliente se haya guardado en la base de datos
            var clienteGuardado = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Contrasenia == "MiContraseñaSegura");
            Assert.NotNull(clienteGuardado);
            Assert.Equal("MiContraseñaSegura", clienteGuardado.Contrasenia);
            Assert.Equal("1234567890", clienteGuardado.Persona.Identificacion); // Verificamos que la identificacion se haya guardado correctamente
        }
    }
}
