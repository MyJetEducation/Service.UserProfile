using Autofac;
using Service.EducationProgress.Client;
using Service.TimeLogger.Client;
using Service.UserProgress.Client;
using Service.UserReward.Client;

namespace Service.UserProfileApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterEducationProgressClient(Program.Settings.EducationProgressServiceUrl);
			builder.RegisterUserProgressClient(Program.Settings.UserProgressServiceUrl);
			builder.RegisterTimeLoggerClient(Program.Settings.TimeLoggerServiceUrl);
			builder.RegisterUserRewardClient(Program.Settings.UserRewardServiceUrl);
		}
	}
}