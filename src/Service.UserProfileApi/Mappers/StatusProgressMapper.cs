using Service.UserProfileApi.Models;
using Service.UserProgress.Grpc.Models;

namespace Service.UserProfileApi.Mappers
{
	public static class StatusProgressMapper
	{
		public static StatusProgressModel ToModel(this ProgressGrpcResponse grpcResponse) => new StatusProgressModel
		{
			Count = grpcResponse.Count,
			Index = grpcResponse.Index,
			Progress = grpcResponse.Progress
		};
	}
}