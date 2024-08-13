using DemoBlazorWithCRUD.Client.Pages;
using DemoBlazorWithCRUD.Components;
using DemoBlazorWithCRUD.Data;
using DemoBlazorWithCRUD.Implementations;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.ProductRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connectionstring DefaultConnection is not found."));
});

// Dependency injection
builder.Services.AddScoped<IProductReposistory, ProductRepository>();

// Add base htttp client address

builder.Services.AddScoped(http => new HttpClient
{
	BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value!)
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(DemoBlazorWithCRUD.Client._Imports).Assembly);

app.Run();
