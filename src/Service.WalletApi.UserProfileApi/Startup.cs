using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyJetWallet.ApiSecurityManager.Autofac;
using MyJetWallet.Sdk.Authorization.Http;
using MyJetWallet.Sdk.GrpcSchema;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.WalletApi;
using Service.Core.Client.Constants;
using Service.WalletApi.UserProfileApi.Modules;
using SimpleTrading.TokensManager;

namespace Service.WalletApi.UserProfileApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            StartupUtils.SetupSimpleServices(services, Program.Settings.SessionEncryptionKeyId);
            services.AddHttpContextAccessor();
            services.ConfigureJetWallet<ApplicationLifetimeManager>(Program.Settings.ZipkinUrl, Configuration.TelemetryPrefix);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                TokensManager.DebugMode = true;
                RootSessionAuthHandler.IsDevelopmentEnvironment = true;
                app.UseDeveloperExceptionPage();
            }

            StartupUtils.SetupWalletApplication(app, env, Program.Settings.EnableApiTrace, "userprofile");
            app.UseEndpoints(endpoints =>
            {
                //security
                endpoints.RegisterGrpcServices();
                endpoints.MapGrpcSchemaRegistry();
                endpoints.MapControllers();
            });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureJetWallet();
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new ClientsModule());
        }
    }
}
