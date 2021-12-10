using System.ServiceModel;
using System.Threading.Tasks;
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