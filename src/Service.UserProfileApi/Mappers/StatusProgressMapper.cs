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

		public static SkillStatusProgressModel ToModel(this SkillProgressGrpcResponse grpcResponse) => new SkillStatusProgressModel
		{
			Total = grpcResponse.Total,
			Activity = grpcResponse.Activity,
			Adaptability = grpcResponse.Adaptability,
			Concentration = grpcResponse.Concentration,
			Memory = grpcResponse.Memory,
			Perseverance = grpcResponse.Perseverance,
			Thoughtfulness = grpcResponse.Thoughtfulness
		};
	}
}