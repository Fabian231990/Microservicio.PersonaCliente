using System.ComponentModel.DataAnnotations;

namespace Microservicio.PersonaCliente.Dominio.Entidades
{
    /// <summary>
    /// Clase Entidad de la tabla Cliente
    /// </summary>
    public class ClienteEntidad
    {
        /// <summary>
        /// Identificador unico del Cliente
        /// </summary>
        [Key]
        public int IdCliente { get; set; }

        /// <summary>
        /// Clave foranea que referencia a Persona
        /// </summary>
        public int IdPersona { get; set; }

        /// <summary>
        /// Contrasenia del Cliente
        /// </summary>
        public string Contrasenia { get; set; }

        /// <summary>
        /// Estado del Cliente
        /// </summary>
        public bool Estado { get; set; }
    }
}
