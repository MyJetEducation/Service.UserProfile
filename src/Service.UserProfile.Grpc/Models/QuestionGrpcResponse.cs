using System.Runtime.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class QuestionGrpcResponse
	{
		[DataMember(Order = 1)]
		public QuestionDataGrpcModel[] Data { get; set; }
	}
}