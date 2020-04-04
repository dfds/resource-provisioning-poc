﻿using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Repositories;
using ResourceProvisioning.Broker.Domain.Services;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;
using ResourceProvisioning.Broker.Infrastructure.Idempotency;

namespace ResourceProvisioning.Broker.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningBroker(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			services.AddOptions();
			services.Configure(configureOptions);

			services.AddBehaviors();
			services.AddCommands();
			services.AddIdempotency();
			services.AddPersistancy(configureOptions);
			services.AddRepositories();
			services.AddServices(); 
		}

		private static void AddBehaviors(this IServiceCollection services)
		{
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(IPipelineBehavior<,>));
		}

		private static void AddCommands(this IServiceCollection services)
		{
			services.AddTransient(typeof(ICommand<>), typeof(ICommand<>));
			services.AddTransient(typeof(ICommandHandler<,>), typeof(ICommandHandler<,>));
		}

		private static void AddIdempotency(this IServiceCollection services)
		{
			services.AddTransient<IRequestManager>();
		}

		private static void AddPersistancy(this IServiceCollection services, System.Action<ProvisioningBrokerOptions> configureOptions)
		{
			var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

			using (var serviceProvider = services.BuildServiceProvider()) 
			{
				var brokerOptions = serviceProvider.GetService<IOptions<ProvisioningBrokerOptions>>()?.Value;

				if (brokerOptions != null)
				{
					services.AddDbContext<DomainContext>(options =>
					{
						options.UseSqlite(brokerOptions.ConnectionStrings.GetValue<string>(nameof(DomainContext)),
											sqliteOptionsAction: sqliteOptions =>
											{
												sqliteOptions.MigrationsAssembly(callingAssemblyName);
												sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");
											});
					}, ServiceLifetime.Scoped);
				}
				else 
				{
					throw new ProvisioningBrokerException("Could not resolve provision broker options");
				}
			}
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(IRepository<>));
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IControlPlaneService>();
		}
	}
}