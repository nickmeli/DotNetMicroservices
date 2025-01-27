using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using static DiscountGrpc.DiscountProtoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Applicaton Services
var assemply = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assemply);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Data Services
builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("ConnectionString")!);
	options.DisableNpgsqlLogging = true;
	options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//	var basketRepository = provider.GetRequiredService<BasketRepository>();
//	return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
//});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Grpc Services
builder.Services.AddGrpcClient<DiscountProtoServiceClient>(options =>
{
	options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
	// Development only bypass
	var handler = new HttpClientHandler
	{
		ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
	};

	return handler;
});

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

// Cross-Cuttong Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("ConnectionString")!)
	.AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
