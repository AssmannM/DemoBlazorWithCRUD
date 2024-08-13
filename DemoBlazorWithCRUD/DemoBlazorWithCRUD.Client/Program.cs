using DemoBlazorWithCRUD.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedLibrary.ProductRepositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IProductReposistory, ProductService>();

builder.Services.AddScoped(http => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});

await builder.Build().RunAsync();
