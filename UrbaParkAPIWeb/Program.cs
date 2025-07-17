using Microsoft.EntityFrameworkCore;
using UrbaPark.Dominio.Servicio.Abstracciones;
using UrbaPark.Dominio.Servicio.Implementaciones;
using UrbaPark.Infraestructura.AccesoDatos;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Infraestructura.AccesoDatos.Repositorio;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.Implementaciones;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseWebRoot("wwwroot");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();



// Configure DbContext
builder.Services.AddDbContext<Pisip_UrbanParkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register custom services
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorioImpl>();
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddScoped<IRolesRepositorio, RolesRepositorioImpl>();
builder.Services.AddScoped<IRolAppService, RolAppService>();
builder.Services.AddScoped<ITipoMantenimientoRepositorio, TipoMantenimientoRepositorioImpl>();
builder.Services.AddScoped<ITipoMantenimientoAppService, TipoMantenimientoAppService>();
builder.Services.AddScoped<IParqueaderoRepositorio, ParqueaderoRepositorioImpl>();
builder.Services.AddScoped<IParqueaderoAppService, ParqueaderoAppService>();
builder.Services.AddScoped<IMantenimientoRepositorio, MantenimientoRepositorioImpl>();
builder.Services.AddScoped<IMantenimientoAppService, MantenimientoAppService>();
builder.Services.AddScoped<IInfo_EncaRepositorio, Info_EncaRepositorioImpl>();
builder.Services.AddScoped<IInformeEncabezadoAppService, InformeEncabezadoAppService>();

builder.Services.AddScoped<IBitacoraRepositorio, BitacoraRepositorioImpl>();
builder.Services.AddScoped<IBitacoraAppService, BitacoraAppService>();
builder.Services.AddScoped<IDet_InfoEncaRepositorio, Det_InfoEncaRepositorioImpl>();
builder.Services.AddScoped<IDetalleInformeAppService, DetalleInformeAppService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();





app.MapControllers();

app.Run();
