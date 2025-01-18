using OrderingApi;
using OrderingApplication;
using OrderingInfrastructure;
using OrderingInfrastructure.DATA.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
	.AddApplicationServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices();

var app = builder.Build();

// Configure HTTP request pipleine
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
}

app.Run();
