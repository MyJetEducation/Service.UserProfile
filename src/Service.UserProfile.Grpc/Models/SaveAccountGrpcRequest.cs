using System;
using System.Runtime.Serialization;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class SaveAccountGrpcRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public string FirstName { get; set; }

		[DataMember(Order = 3)]
		public string LastName { get; set; }

		[DataMember(Order = 4)]
		public string Gender { get; set; }

		[DataMember(Order = 5)]
		public string Phone { get; set; }

		[DataMember(Order = 6)]
		public string Country { get; set; }
	}
}