﻿using System;
using System.Net;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MySettingsReader;
using Service.Core.Client.Constants;
using Service.Core.Client.Helpers;
using Service.WalletApi.UserProfileApi.Settings;

namespace Service.WalletApi.UserProfileApi
{
	public class Program
	{
		public static SettingsModel Settings { get; private set; }
		public static Func<T> ReloadedSettings<T>(Func<SettingsModel, T> getter) => () => getter.Invoke(GetSettings());
		private static SettingsModel GetSettings() => SettingsReader.GetSettings<SettingsModel>(ProgramHelper.SettingsFileName);

		public static ILoggerFactory LoggerFactory { get; private set; }

		public static void Main(string[] args)
		{
			Console.Title = "MyJetWallet Service.Wallet.Api.UserProfileApi";

			Settings = GetSettings();

			using ILoggerFactory loggerFactory = LogConfigurator.ConfigureElk(Configuration.ProductName, Settings.SeqServiceUrl, Settings.ElkLogs);

			LoggerFactory = loggerFactory;

			ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

			try
			{
				logger.LogInformation("Application is being started");

				CreateHostBuilder(loggerFactory, args).Build().Run();

				logger.LogInformation("Application has been stopped");
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Application has been terminated unexpectedly");
			}
		}

		public static IHostBuilder CreateHostBuilder(ILoggerFactory loggerFactory, string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureKestrel(options =>
					{
						string httpPort = Environment.GetEnvironmentVariable("HTTP_PORT") ?? "8080";
						string grpcPort = Environment.GetEnvironmentVariable("GRPC_PORT") ?? "80";

						Console.WriteLine($"HTTP PORT: {httpPort}");
						Console.WriteLine($"GRPC PORT: {grpcPort}");

						options.Listen(IPAddress.Any, int.Parse(httpPort), o => o.Protocols = HttpProtocols.Http1);
						options.Listen(IPAddress.Any, int.Parse(grpcPort), o => o.Protocols = HttpProtocols.Http2);
					});

					webBuilder.UseStartup<Startup>();
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(loggerFactory);
					services.AddSingleton(typeof (ILogger<>), typeof (Logger<>));
				});
	}
}