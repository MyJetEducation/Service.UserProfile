using Autofac;
using Service.UserProfile.Services;

namespace Service.UserProfile.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<UserProfileQuestionRepository>().AsImplementedInterfaces().SingleInstance();
		}
	}
}