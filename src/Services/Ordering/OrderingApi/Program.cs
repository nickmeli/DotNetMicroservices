using OrderingApi;
using OrderingApplication;
using OrderingInfrastructure;
using OrderingInfrastructure.DATA.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
	.AddApplicationServices(builder.Configuration)
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure HTTP request pipleine
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
}

app.Run();
