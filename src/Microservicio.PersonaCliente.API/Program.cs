using Microservicio.PersonaCliente.Aplicacion.Interfaces;
using Microservicio.PersonaCliente.Aplicacion.Servicios;
using Microservicio.PersonaCliente.Infraestructura.Interfaces;
using Microservicio.PersonaCliente.Infraestructura.Persistencia;
using Microservicio.PersonaCliente.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder constructor = WebApplication.CreateBuilder(args);

constructor.Services.AddControllers();
constructor.Services.AddEndpointsApiExplorer();
constructor.Services.AddSwaggerGen();

constructor.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
constructor.Services.AddScoped<IPersonaServicio, PersonaServicio>();
constructor.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
constructor.Services.AddScoped<IClienteServicio, ClienteServicio>();

constructor.Services.AddDbContext<EjercicioTecnicoDBContext>(options =>
    options.UseSqlServer(constructor.Configuration.GetConnectionString("Conexion")));

WebApplication aplicacion = constructor.Build();

if (aplicacion.Environment.IsDevelopment())
{
    aplicacion.UseSwagger();
    aplicacion.UseSwaggerUI();
}

aplicacion.UseHttpsRedirection();
aplicacion.UseAuthorization();
aplicacion.MapControllers();
aplicacion.Run();
