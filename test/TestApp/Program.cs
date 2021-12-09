using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Client;
using Service.UserProfile.Client;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;
using Service.UserProfile.Postgres;

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

			//Create some questions
			CreateTestQuestion();

			//Get this questions
			Console.WriteLine($"{Environment.NewLine}Get questions");
			QuestionGrpcResponse questionResponse = await client.GetQuestions();
			LogData(questionResponse);

			//Save account
			Console.WriteLine($"{Environment.NewLine}Save new account");
			var userId = new Guid("60dde041-529c-4f5a-85bf-676858dc2f4f");
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
			AccountGrpcResponse getAccountResponse2 = await client.GetAccount(new GetAccountGrpcRequest { UserId = userId });
			LogData(getAccountResponse2);

			Console.WriteLine("End");
			Console.ReadLine();
		}

		private static void CreateTestQuestion()
		{
			DatabaseContext context = GetDbContext();

			QuestionEntity[] array = context.Questions.ToArray();
			if (array.Any())
			{
				context.Questions.RemoveRange(array);

				context.SaveChanges();
			}

			context.Questions.Add(new QuestionEntity
			{
				Id = 100,
				AdditionalAnswer = true,
				AnswerName = "some_answer",
				AnswerType = "some_type",
				Enabled = true,
				Order = 1,
				Title = "title",
				AnswerData = JsonSerializer.Serialize(new[]
				{
					new QuestionAnswerDataGrpcModel
					{
						Id = 1,
						Label = "answer_label",
						Value = "answer_value"
					}
				})
			});

			context.SaveChanges();
		}

		private static DatabaseContext GetDbContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=education");
			var context = new DatabaseContext(optionsBuilder.Options);
			return context;
		}

		private static void LogData(object data) => Console.WriteLine(JsonSerializer.Serialize(data));
	}
}