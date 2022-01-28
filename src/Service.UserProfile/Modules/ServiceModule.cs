using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;
using Service.Core.Client.Services;
using Service.ServiceBus.Models;
using Service.UserProfile.Postgres.Services;

namespace Service.UserProfile.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AccountRepository>().AsImplementedInterfaces().SingleInstance();
			builder.Register(context => new EncoderDecoder(Program.EncodingKey)).As<IEncoderDecoder>().SingleInstance();

			var tcpServiceBus = new MyServiceBusTcpClient(() => Program.Settings.ServiceBusWriter, "MyJetEducation Service.UserProfile");

			builder
				.Register(context => new MyServiceBusPublisher<UserAccountFilledServiceBusModel>(tcpServiceBus, UserAccountFilledServiceBusModel.TopicName, false))
				.As<IServiceBusPublisher<UserAccountFilledServiceBusModel>>()
				.SingleInstance();

			tcpServiceBus.Start();
		}
	}
}