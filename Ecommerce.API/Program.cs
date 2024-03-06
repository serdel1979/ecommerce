

using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using Ecommerce.Repositorio.Implementacion;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<EcommerceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddTransient(typeof(IGenericoRepositorio<>),typeof(GenericoRepositorio<>));
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();

builder.Services.AddScoped<IProductoServicio, ProductoServicio>();

builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();

builder.Services.AddScoped<IVentaServicio, VentaServicio>();

builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();


builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("nueva", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors("nueva");

app.UseAuthorization();

app.MapControllers();

app.Run();
