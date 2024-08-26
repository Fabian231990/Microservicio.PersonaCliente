namespace Microservicio.PersonaCliente.Dominio.Dto
{
    /// <summary>
    /// DTO para la entidad Cliente.
    /// </summary>
    public class ClienteDto
    {
        /// <summary>
        /// Identificador unico del cliente.
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Identificador unico de la persona.
        /// </summary>
        public int IdPersona { get; set; }

        /// <summary>
        /// Numero de identificacion unico de la persona.
        /// </summary>
        public string Identificacion { get; set; }

        /// <summary>
        /// Contraseña del cliente para acceder a sus servicios.
        /// </summary>
        public string Contrasenia { get; set; }

        /// <summary>
        /// Estado del cliente. Indica si el cliente esta activo o inactivo.
        /// </summary>
        public bool Estado { get; set; }
    }
}
