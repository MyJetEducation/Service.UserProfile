using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class QuestionDataGrpcModel
	{
		[DataMember(Order = 1)]
		[JsonPropertyName("id")]
		public int? Id { get; set; }

		[DataMember(Order = 2)]
		[JsonPropertyName("title")]
		public string Title { get; set; }

		[DataMember(Order = 3)]
		[JsonPropertyName("answer")]
		public QuestionAnswerGrpcModel QuestionAnswer { get; set; }
	}
}