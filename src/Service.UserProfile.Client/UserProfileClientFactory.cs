using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.UserProfile.Grpc;

namespace Service.UserProfile.Client
{
	[UsedImplicitly]
	public class UserProfileClientFactory : MyGrpcClientFactory
	{
		public UserProfileClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
		{
		}

		public IUserProfileService GetUserProfileService() => CreateGrpcService<IUserProfileService>();
	}
}