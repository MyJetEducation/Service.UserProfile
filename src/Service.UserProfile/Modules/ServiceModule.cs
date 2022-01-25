using Autofac;
using DotNetCoreDecorators;
using MyServiceBus.TcpClient;
using Service.Core.Domain;
using Service.Core.Domain.Models;
using Service.UserProfile.Domain;
using Service.UserProfile.Grpc.ServiceBusModel;
using Service.UserProfile.Services;

namespace Service.UserProfile.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AccountRepository>().AsImplementedInterfaces().SingleInstance();
			builder.Register(context => new EncoderDecoder(Program.EncodingKey)).As<IEncoderDecoder>().SingleInstance();

			var tcpServiceBus = new MyServiceBusTcpClient(() => Program.Settings.ServiceBusWriter, "MyJetEducation Service.UserProfile");
			IPublisher<UserAccountFilledServiceBusModel> clientRegisterPublisher = new MyServiceBusPublisher(tcpServiceBus);
			builder.Register(context => clientRegisterPublisher);
			tcpServiceBus.Start();
		}
	}
}