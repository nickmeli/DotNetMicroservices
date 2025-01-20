﻿
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingApplication.DATA;

namespace OrderingInfrastructure;

/**
 * Migrations command
 * Add-Migration InitialCreate -OutputDir DATA/Migrations -Project OrderingInfrastructure -StartupProject OrderingApi
 */

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices
		(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Database");

		// Add services to the container
		services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
		services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

		services.AddDbContext<ApplicationDbContext>((sp, options) =>
		{
			//options.AddInterceptors(
			//	new AuditableEntityInterceptor(),
			//	new DispatchDomainEventsInterceptor()
			//);
			options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
			options.UseSqlServer(connectionString);
		});
		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

		return services;
	}
}
