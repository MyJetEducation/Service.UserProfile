using System.ServiceModel;
using System.Threading.Tasks;
using Service.Core.Grpc.Models;
using Service.UserProfile.Grpc.Models;

namespace Service.UserProfile.Grpc
{
	[ServiceContract]
	public interface IUserProfileService
	{
		[OperationContract]
		ValueTask<AccountGrpcResponse> GetAccount(GetAccountGrpcRequest request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> SaveAccount(SaveAccountGrpcRequest request);
	}
}