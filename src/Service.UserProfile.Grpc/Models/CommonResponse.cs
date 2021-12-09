using System.Runtime.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class CommonResponse
	{
		[DataMember(Order = 1)]
		public bool IsSuccess { get; set; }
	}
}