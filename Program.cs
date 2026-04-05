using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SocioWeb;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels;
using SocioWeb.ViewModels.Appointments;
using SocioWeb.ViewModels.Clinic;
using SocioWeb.ViewModels.Clinic;
using SocioWeb.ViewModels.Employee;
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
    new HttpClient { BaseAddress = new Uri("http://localhost:8080/") });

// Servicios de dominio
builder.Services.AddScoped<IAppointmentService, AppointmentsApiService>();
builder.Services.AddScoped<IOwnerService, OwnersApiService>();
builder.Services.AddScoped<IPetService, PetsApiService>();
builder.Services.AddScoped<IProductService, ProductsApiService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddTransient<ClinicprofileViewModel>();

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
builder.Services.AddScoped<IEmployeeService, EmployeesApiService>();
builder.Services.AddTransient<EmployeeListViewModel>();
builder.Services.AddScoped<IEmployeeService, EmployeesApiService>();
builder.Services.AddTransient<EmployeeFormVM>();
builder.Services.AddTransient<EmployeeProfileVM>();
builder.Services.AddTransient<EmployeeListViewModel>();

await builder.Build().RunAsync();