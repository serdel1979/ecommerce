using Blazored.LocalStorage;
using Blazored.Toast;
using Ecommerce.WebAssembly;
using Ecommerce.WebAssembly.Servicios.Contrato;
using Ecommerce.WebAssembly.Servicios.Implementacion;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7295/api/") });


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();


builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
