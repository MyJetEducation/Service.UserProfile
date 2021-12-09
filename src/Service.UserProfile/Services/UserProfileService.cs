using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;

namespace Service.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IUserProfileQuestionRepository _userProfileQuestionRepository;

		public UserProfileService(IUserProfileQuestionRepository userProfileQuestionRepository)
		{
			_userProfileQuestionRepository = userProfileQuestionRepository;
		}

		public async ValueTask<UserProfileQuestionGrpcResponse> GetQuestions()
		{
			UserProfileQuestionEntity[] questions = await _userProfileQuestionRepository.GetQuestions();

			return new UserProfileQuestionGrpcResponse
			{
				Data = questions.Select(questionEntity => new UserProfileQuestionDataGrpcModel
				{
					Id = questionEntity.Id,
					Title = questionEntity.Title,
					QuestionAnswer = new UserProfileQuestionAnswerGrpcModel
					{
						AdditionalAnswer = questionEntity.AdditionalAnswer,
						AnswerType = questionEntity.AnswerType,
						AnswerName = questionEntity.AnswerName,
						AnswerData = JsonSerializer.Deserialize<UserProfileQuestionAnswerDataGrpcModel[]>(questionEntity.AnswerData)
					}
				}).ToArray()
			};
		}
	}
}