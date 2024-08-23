using Microsoft.EntityFrameworkCore;
using Microservicio.PersonaCliente.Dominio.Entidades;

namespace Microservicio.PersonaCliente.Infraestructura.Persistencia
{
    public class EjercicioTecnicoDBContext : DbContext
    {
        public EjercicioTecnicoDBContext(DbContextOptions<EjercicioTecnicoDBContext> options) : base(options) { }

        public DbSet<PersonaEntidad> Persona { get; set; }
    }
}
