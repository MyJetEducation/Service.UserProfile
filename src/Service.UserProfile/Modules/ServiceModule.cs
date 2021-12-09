using Autofac;
using Service.UserProfile.Domain;
using Service.UserProfile.Domain.Models;

namespace Service.UserProfile.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<QuestionRepository>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<AccountRepository>().AsImplementedInterfaces().SingleInstance();

			builder.Register(context => new EncoderDecoder(Program.EncodingKey))
				.As<IEncoderDecoder>()
				.SingleInstance();
		}
	}
}