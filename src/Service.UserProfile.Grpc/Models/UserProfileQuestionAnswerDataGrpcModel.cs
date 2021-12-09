using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class UserProfileQuestionAnswerDataGrpcModel
	{
		[DataMember(Order = 1)]
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[DataMember(Order = 2)]
		[JsonPropertyName("label")]
		public string Label { get; set; }

		[DataMember(Order = 3)]
		[JsonPropertyName("value")]
		public string Value { get; set; }
	}
}