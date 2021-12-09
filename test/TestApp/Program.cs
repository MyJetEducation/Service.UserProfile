using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProtoBuf.Grpc.Client;
using Service.UserProfile.Client;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;

namespace TestApp
{
	internal class Program
	{
		private static async Task Main()
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			Console.Write("Press enter to start");
			Console.ReadLine();

			var factory = new UserProfileClientFactory("http://localhost:5001");
			IUserProfileService client = factory.GetUserProfileService();

			UserProfileQuestionGrpcResponse userProfileQuestionGrpcResponse = await client.GetQuestions();
			Console.WriteLine(JsonConvert.SerializeObject(userProfileQuestionGrpcResponse));

			Console.WriteLine("End");
			Console.ReadLine();
		}
	}
}