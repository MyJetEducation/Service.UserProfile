using Autofac;
using MyJetWallet.ApiSecurityManager.Autofac;
using MyJetWallet.Sdk.RestApiTrace;
using Service.EducationProgress.Client;
using Service.TimeLogger.Client;
using Service.UserProgress.Client;
using Service.UserReward.Client;

namespace Service.WalletApi.UserProfileApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterEncryptionServiceClient();

			builder.RegisterEducationProgressClient(Program.Settings.EducationProgressServiceUrl);
			builder.RegisterUserProgressClient(Program.Settings.UserProgressServiceUrl);
			builder.RegisterTimeLoggerClient(Program.Settings.TimeLoggerServiceUrl);
			builder.RegisterUserRewardClient(Program.Settings.UserRewardServiceUrl);

			if (Program.Settings.EnableApiTrace)
			{
				builder
					.RegisterInstance(new ApiTraceManager(Program.Settings.ElkLogs, "api-trace",
						Program.LoggerFactory.CreateLogger("ApiTraceManager")))
					.As<IApiTraceManager>()
					.As<IStartable>()
					.AutoActivate()
					.SingleInstance();
			}
		}
	}
}