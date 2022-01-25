using System.Threading.Tasks;
using DotNetCoreDecorators;
using Service.Core.Domain.Models;
using Service.Core.Grpc.Models;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;
using Service.UserProfile.Grpc.ServiceBusModel;
using Service.UserProfile.Mappers;

namespace Service.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IEncoderDecoder _encoderDecoder;
		private readonly IPublisher<UserAccountFilledServiceBusModel> _publisher;

		public UserProfileService(IAccountRepository accountRepository, IEncoderDecoder encoderDecoder, IPublisher<UserAccountFilledServiceBusModel> publisher)
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