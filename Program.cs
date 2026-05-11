using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SocioWeb;
using SocioWeb.Entities.Models.Auth;
using SocioWeb.Infrastructure;
using SocioWeb.Infrastructure.Auth;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels;
using SocioWeb.ViewModels.Appointments;
using SocioWeb.ViewModels.Clinic;
using SocioWeb.ViewModels.Employee;
using SocioWeb.ViewModels.Auth;
using SocioWeb.ViewModels.LoginVM;
using SocioWeb.ViewModels.Medical;
using SocioWeb.ViewModels.Owners;
using SocioWeb.ViewModels.OwnerVM;
using SocioWeb.ViewModels.Pets;
using SocioWeb.ViewModels.Products;
using Microsoft.AspNetCore.Components.Authorization;
using SocioWeb.Infrastructure.Auth;
using SocioWeb.Services.PetService;
using SocioWeb.Infrastructure.Services.ClinicService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CookieHandler>();

// HttpClient para API backend
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<CookieHandler>();
    handler.InnerHandler = new HttpClientHandler();
    
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:8080/")
    };
});

// Servicios de dominio
builder.Services.AddScoped<IAppointmentService, AppointmentsApiService>();
builder.Services.AddScoped<IOwnerService, OwnersApiService>();
builder.Services.AddScoped<IPetService, PetsApiService>();
builder.Services.AddScoped<IProductService, ProductsApiService>();
builder.Services.AddScoped<IAuthService, AuthApiService>();
// Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


builder.Services.AddRadzenComponents(); // O los servicios individuales como NotificationService

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IClinicService, ClinicApiService>();
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
builder.Services.AddScoped<OnboardingState>();
builder.Services.AddScoped<LoginVM>();
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
builder.Services.AddScoped<RegisterClinicVM>();
builder.Services.AddScoped<VerifyEmailVM>();
builder.Services.AddScoped<CompleteProfileVM>();
builder.Services.AddScoped<ForgotPasswordVM>();
builder.Services.AddScoped<ResetPasswordVM>();

await builder.Build().RunAsync();