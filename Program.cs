using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SocioWeb;
using SocioWeb.Services.AppointmentService;
using SocioWeb.ViewModels;
using SocioWeb.ViewModels.OwnerVM;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient para API backend
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

// Servicios de dominio
builder.Services.AddScoped<IAppointmentService, AppointmentsApiService>();
builder.Services.AddScoped<IOwnerService, OwnersApiService>();
builder.Services.AddScoped<IPetService, PetsApiService>();
builder.Services.AddScoped<IProductService, ProductsApiService>();

// ViewModels
builder.Services.AddScoped<OwnerPageVM>();
builder.Services.AddScoped<OwnerProfileVM>();
builder.Services.AddScoped<MedicalRegisterVM>();
builder.Services.AddScoped<PetsMenuVM>();

await builder.Build().RunAsync();