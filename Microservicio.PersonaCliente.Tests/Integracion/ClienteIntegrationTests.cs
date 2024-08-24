using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Dominio.Entidades;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Microservicio.PersonaCliente.Tests.Integracion
{
    public class ClienteIntegrationTests
    {
        private EjercicioTecnicoDBContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EjercicioTecnicoDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new EjercicioTecnicoDBContext(options);
        }

        [Fact]
        public async Task CrearCliente_DeberiaGuardarEnLaBaseDeDatos()
        {
            // Arrange: Configuramos el contexto en memoria y el repositorio
            var dbContext = GetInMemoryDbContext();
            var clienteRepositorio = new ClienteRepositorio(dbContext);
            var clienteService = new ClienteServicio(clienteRepositorio);

            var nuevoCliente = new ClienteEntidad
            {
                IdPersona = 1,
                Contrasenia = "MiContraseñaSegura",
                Estado = true
            };

            // Act: Ejecutamos el método que estamos probando
            var resultado = await clienteService.CrearClienteAsync(nuevoCliente);

            // Assert: Verificamos que el cliente se haya guardado en la base de datos
            var clienteGuardado = await dbContext.Cliente.FirstOrDefaultAsync(c => c.IdPersona == 1);
            Assert.NotNull(clienteGuardado);
            Assert.Equal("MiContraseñaSegura", clienteGuardado.Contrasenia);
        }
    }
}