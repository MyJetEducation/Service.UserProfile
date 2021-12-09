using System;
using System.Runtime.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class GetAccountGrpcRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }
	}
}