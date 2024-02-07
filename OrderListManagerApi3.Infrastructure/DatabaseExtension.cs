using System;
using Microsoft.Extensions.DependencyInjection;
using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Infrastructure
{
	public static class DatabaseExtension
	{ 
		public static void AddDatabaseServices(this IServiceCollection service)
		{
			service.AddSingleton<JsonLocalFileGenerator>();
		}
	}
}

