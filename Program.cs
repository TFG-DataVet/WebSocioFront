using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SocioWeb;
using SocioWeb.Services.AppointmentService;
using SocioWeb.ViewModels;
using SocioWeb.ViewModels.Appointments;
using SocioWeb.ViewModels.Medical;
using SocioWeb.ViewModels.Owners;
using SocioWeb.ViewModels.OwnerVM;
using SocioWeb.ViewModels.Pets;
using SocioWeb.ViewModels.Products;

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
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<PetPageVM>();
builder.Services.AddScoped<OwnerFormularyVM>();
builder.Services.AddTransient<AppointmentListViewModel>();
builder.Services.AddTransient<AppointmentProfileViewModel>();
builder.Services.AddTransient<CreateAppointmentViewModel>();
builder.Services.AddTransient<PetListViewModel>();
builder.Services.AddTransient<PetProfileViewModel>();
builder.Services.AddTransient<OwnerListViewModel>();
builder.Services.AddTransient<OwnerProfileViewModel>();
builder.Services.AddTransient<ProductListViewModel>();
builder.Services.AddTransient<ProductProfileViewModel>();
builder.Services.AddTransient<MedicalRegisterViewModel>();

await builder.Build().RunAsync();