using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;
using Service.UserProfile.Grpc.ServiceBusModel;

namespace Service.UserProfile.Services
{
	public class MyServiceBusPublisher : IPublisher<UserAccountFilledServiceBusModel>
	{
		private readonly MyServiceBusTcpClient _client;

		public MyServiceBusPublisher(MyServiceBusTcpClient client)
		{
			_client = client;
			_client.CreateTopicIfNotExists(UserAccountFilledServiceBusModel.TopicName);
		}

		public ValueTask PublishAsync(UserAccountFilledServiceBusModel valueToPublish)
		{
			byte[] bytesToSend = valueToPublish.ServiceBusContractToByteArray();

			Task task = _client.PublishAsync(UserAccountFilledServiceBusModel.TopicName, bytesToSend, false);

			return new ValueTask(task);
		}
	}
}