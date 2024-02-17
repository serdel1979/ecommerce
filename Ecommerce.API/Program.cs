

using Ecommerce.Repositorio.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<EcommerceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CadenaSQL"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
