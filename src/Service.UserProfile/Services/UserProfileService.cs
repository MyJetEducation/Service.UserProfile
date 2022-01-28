using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.ServiceBus;
using Service.Core.Client.Models;
using Service.Core.Client.Services;
using Service.ServiceBus.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;
using Service.UserProfile.Mappers;
using Service.UserProfile.Postgres.Models;
using Service.UserProfile.Postgres.Services;

namespace Service.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IEncoderDecoder _encoderDecoder;
		private readonly IServiceBusPublisher<UserAccountFilledServiceBusModel> _publisher;

		public UserProfileService(IAccountRepository accountRepository, IEncoderDecoder encoderDecoder, IServiceBusPublisher<UserAccountFilledServiceBusModel> publisher)
		{
			_accountRepository = accountRepository;
			_encoderDecoder = encoderDecoder;
			_publisher = publisher;
		}

		public async ValueTask<CommonGrpcResponse> SaveAccount(SaveAccountGrpcRequest request)
		{
			bool saved = await _accountRepository.SaveAccount(request.ToEntity(_encoderDecoder));

			if (saved && request.IsFilled())
				await _publisher.PublishAsync(new UserAccountFilledServiceBusModel {UserId = request.UserId});

			return CommonGrpcResponse.Result(saved);
		}

		public async ValueTask<AccountGrpcResponse> GetAccount(GetAccountGrpcRequest request)
		{
			AccountEntity accountEntity = await _accountRepository.GetAccount(request.UserId);

			return new AccountGrpcResponse
			{
				Data = accountEntity?.ToGrpcModel(_encoderDecoder)
			};
		}
	}
}