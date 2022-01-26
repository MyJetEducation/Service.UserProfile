using System;
using System.Text.Json;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.Core.Client.Models;
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

			//Save account
			Console.WriteLine($"{Environment.NewLine}Save new account");
			var userId = Guid.NewGuid();
			CommonGrpcResponse saveAccountResponse1 = await client.SaveAccount(new SaveAccountGrpcRequest
			{
				UserId = userId,
				FirstName = "Name1",
				LastName = "Name2",
				Gender = "male",
				Phone = "911",
				Country = "EU"
			});
			LogData(saveAccountResponse1);

			//Get this account
			Console.WriteLine($"{Environment.NewLine}Get saved account");
			AccountGrpcResponse getAccountResponse1 = await client.GetAccount(new GetAccountGrpcRequest {UserId = userId});
			LogData(getAccountResponse1);

			//Update account
			Console.WriteLine($"{Environment.NewLine}Update existing account");
			CommonGrpcResponse saveAccountResponse2 = await client.SaveAccount(new SaveAccountGrpcRequest
			{
				UserId = userId,
				FirstName = "Name10",
				LastName = "Name20",
				Gender = "female",
				Phone = "02",
				Country = "USA"
			});
			LogData(saveAccountResponse2);

			//Get this account
			Console.WriteLine($"{Environment.NewLine}Get updated account");
			AccountGrpcResponse getAccountResponse2 = await client.GetAccount(new GetAccountGrpcRequest {UserId = userId});
			LogData(getAccountResponse2);

			Console.WriteLine("End");
			Console.ReadLine();
		}

		private static void LogData(object data) => Console.WriteLine(JsonSerializer.Serialize(data));
	}
}