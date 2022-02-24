﻿using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyJetWallet.Sdk.GrpcSchema;
using MyJetWallet.Sdk.Service;
using Prometheus;
using Service.UserProfileApi.Modules;
using SimpleTrading.ServiceStatusReporterConnector;

namespace Service.UserProfileApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.BindCodeFirstGrpc();
            services.AddHostedService<ApplicationLifetimeManager>();
            services.AddMyTelemetry("ED-", Program.Settings.ZipkinUrl);
            services.SetupSwaggerDocumentation();
            services.ConfigurateHeaders();
            services.AddControllers();

            services
                .AddAuthentication(StartupUtils.ConfigureAuthenticationOptions)
                .AddJwtBearer(StartupUtils.ConfigureJwtBearerOptions);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseForwardedHeaders();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseMetricServer();
            app.BindServicesTree(Assembly.GetExecutingAssembly());
            app.BindIsAlive();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthentication();
            app.UseAuthorization();
            app.SetupSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("MyJetEducation API endpoint"));
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}