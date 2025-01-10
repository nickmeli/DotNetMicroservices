var builder = WebApplication.CreateBuilder(args);

// Add services to container
var programAssembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(programAssembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(programAssembly);
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("ConnectionString")!);
	options.DisableNpgsqlLogging = true;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("ConnectionString")!);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
	new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
	}
);

app.Run();
