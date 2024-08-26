using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Tests.Test
{
    /// <summary>
    /// Tests Persona Entidad
    /// </summary>
    public class PersonaEntidadTests
    {
        [Fact]
        public void PersonaEntidad_CrearPersona_PropiedadesAsignadasCorrectamente()
        {
            // Arrange: Configuramos los datos de entrada
            string nombre = "Juan Perez";
            string genero = "Masculino";
            int edad = 30;
            string identificacion = "1234567890";
            string direccion = "Quito, Ecuador";
            string telefono = "0987654321";

            // Act: Creamos una instancia de PersonaEntidad
            var persona = new PersonaEntidad
            {
                Nombre = nombre,
                Genero = genero,
                Edad = edad,
                Identificacion = identificacion,
                Direccion = direccion,
                Telefono = telefono
            };

            // Assert: Verificamos que las propiedades se hayan asignado correctamente
            Assert.Equal(nombre, persona.Nombre);
            Assert.Equal(genero, persona.Genero);
            Assert.Equal(edad, persona.Edad);
            Assert.Equal(identificacion, persona.Identificacion);
            Assert.Equal(direccion, persona.Direccion);
            Assert.Equal(telefono, persona.Telefono);
        }

        [Fact]
        public void PersonaEntidad_ValidarEdadNegativa_DeberiaFallar()
        {
            // Arrange
            var persona = new PersonaEntidad();

            // Act & Assert: Validamos que establecer una edad negativa genera una excepcion
            Assert.Throws<ArgumentOutOfRangeException>(() => persona.Edad = -1);
        }

        [Fact]
        public void PersonaEntidad_ValidarIdentificacionLongitudIncorrecta_DeberiaFallar()
        {
            // Arrange
            var persona = new PersonaEntidad();

            // Act & Assert: Validamos que establecer una identificacion con longitud incorrecta genera una excepcion
            Assert.Throws<ArgumentException>(() => persona.Identificacion = "123");
        }
    }
}