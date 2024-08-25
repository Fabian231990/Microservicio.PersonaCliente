using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Tests.Test
{
    /// <summary>
    /// Tests Cliente Entidad
    /// </summary>
    public class ClienteEntidadTests
    {
        [Fact]
        public void ClienteEntidad_CrearCliente_PropiedadesAsignadasCorrectamente()
        {
            // Arrange: Configuramos los datos de entrada
            int idPersona = 1;
            string contrasenia = "MiContraseñaSegura";
            bool estado = true;

            // Act: Creamos una instancia de ClienteEntidad
            var cliente = new ClienteEntidad
            {
                IdPersona = idPersona,
                Contrasenia = contrasenia,
                Estado = estado
            };

            // Assert: Verificamos que las propiedades se hayan asignado correctamente
            Assert.Equal(idPersona, cliente.IdPersona);
            Assert.Equal(contrasenia, cliente.Contrasenia);
            Assert.True(cliente.Estado);
        }

        [Fact]
        public void ClienteEntidad_CrearCliente_SinContrasenia_LanzaExcepcion()
        {
            // Arrange
            int idPersona = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ClienteEntidad
            {
                IdPersona = idPersona,
                Contrasenia = string.Empty, // Esta contraseña debería causar una excepción
                Estado = true
            });
        }
    }
}