using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class QuestionAnswerGrpcModel
	{
		[DataMember(Order = 1)]
		[JsonPropertyName("type")]
		public string AnswerType { get; set; }

		[DataMember(Order = 2)]
		[JsonPropertyName("name")]
		public string AnswerName { get; set; }

		[DataMember(Order = 3)]
		[JsonPropertyName("additional")]
		public bool? AdditionalAnswer { get; set; }

		[DataMember(Order = 4)]
		[JsonPropertyName("data")]
		public QuestionAnswerDataGrpcModel[] AnswerData { get; set; }
	}
}