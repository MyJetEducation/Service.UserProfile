using System.Runtime.Serialization;
using Service.UserProfile.Domain.Models;

namespace Service.UserProfile.Grpc.Models
{
	[DataContract]
	public class HelloMessage : IHelloMessage
	{
		[DataMember(Order = 1)]
		public string Message { get; set; }
	}
}