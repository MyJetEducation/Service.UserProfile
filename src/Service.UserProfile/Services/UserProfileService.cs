using System.Threading.Tasks;
using Service.Core.Domain.Models;
using Service.Core.Grpc.Models;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;
using Service.UserProfile.Mappers;

namespace Service.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IEncoderDecoder _encoderDecoder;

		public UserProfileService(IAccountRepository accountRepository, IEncoderDecoder encoderDecoder)
		{
			_accountRepository = accountRepository;
			_encoderDecoder = encoderDecoder;
		}

		public async ValueTask<CommonGrpcResponse> SaveAccount(SaveAccountGrpcRequest request)
		{
			bool saved = await _accountRepository.SaveAccount(request.ToEntity(_encoderDecoder));

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