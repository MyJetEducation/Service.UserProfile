using System.Linq;
using System.Threading.Tasks;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;
using Service.UserProfile.Mappers;

namespace Service.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IQuestionRepository _questionRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IEncoderDecoder _encoderDecoder;

		public UserProfileService(IQuestionRepository questionRepository, IAccountRepository accountRepository, IEncoderDecoder encoderDecoder)
		{
			_questionRepository = questionRepository;
			_accountRepository = accountRepository;
			_encoderDecoder = encoderDecoder;
		}

		public async ValueTask<QuestionGrpcResponse> GetQuestions()
		{
			QuestionEntity[] questions = await _questionRepository.GetQuestions();

			return new QuestionGrpcResponse
			{
				Data = questions?.Select(questionEntity => questionEntity.ToGrpcModel()).ToArray()
			};
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