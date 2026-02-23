using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SocioWeb;
using SocioWeb.Services.AppointmentService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<AppointmentService>();

await builder.Build().RunAsync();


builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5001/") // backend real
    });

builder.Services.AddScoped<IAppointmentService, AppointmentsApiService>();
builder.Services.AddScoped<IOwnerService, OwnersApiService>();
builder.Services.AddScoped<IPetService, PetsApiService>();
builder.Services.AddScoped<IProductService, ProductsApiService>();