using System.ComponentModel.DataAnnotations;

namespace Microservicio.PersonaCliente.Dominio.Entidades
{
    /// <summary>
    /// Clase Entidad de la tabla Persona
    /// </summary>
    public class PersonaEntidad
    {
        /// <summary>
        /// Identificador unico de la Persona
        /// </summary>
        [Key]
        public int IdPersona { get; set; }

        /// <summary>
        /// Nombre de la persona
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Genero de la Persona
        /// </summary>
        public string Genero { get; set; }

        /// <summary>
        /// Edad de la Persona
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Identificacion de la Persona
        /// </summary>
        public string Identificacion { get; set; }

        /// <summary>
        /// Direccion de la Persona
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Telefono de la Persona
        /// </summary>
        public string Telefono { get; set; }
    }
}
