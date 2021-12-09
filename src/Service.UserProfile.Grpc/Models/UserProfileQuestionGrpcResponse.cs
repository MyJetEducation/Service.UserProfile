using System.Runtime.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class UserProfileQuestionGrpcResponse
	{
		[DataMember(Order = 1)]
		public UserProfileQuestionDataGrpcModel[] Data { get; set; }
	}
}