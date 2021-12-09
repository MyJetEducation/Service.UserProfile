using System.Text.Json;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc.Models;

namespace Service.UserProfile.Mappers
{
	public static class QuestionMapper
	{
		public static QuestionDataGrpcModel ToGrpcModel(this QuestionEntity entity) => new QuestionDataGrpcModel
		{
			Id = entity.Id,
			Title = entity.Title,
			QuestionAnswer = new QuestionAnswerGrpcModel
			{
				AdditionalAnswer = entity.AdditionalAnswer,
				AnswerType = entity.AnswerType,
				AnswerName = entity.AnswerName,
				AnswerData = JsonSerializer.Deserialize<QuestionAnswerDataGrpcModel[]>(entity.AnswerData)
			}
		};
	}
}