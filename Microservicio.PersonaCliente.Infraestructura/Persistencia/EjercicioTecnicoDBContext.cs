using Microsoft.EntityFrameworkCore;
using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Infraestructura.Persistencia
{
    public class EjercicioTecnicoDBContext(DbContextOptions<EjercicioTecnicoDBContext> options) : DbContext(options)
    {
        // DbSet para Persona
        public DbSet<PersonaEntidad> Persona { get; set; }

        // DbSet para Cliente
        public DbSet<ClienteEntidad> Cliente { get; set; }
    }
}
