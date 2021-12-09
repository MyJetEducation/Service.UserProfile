using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Postgres;

namespace Service.UserProfile.Services
{
	public class UserProfileQuestionRepository : IUserProfileQuestionRepository
	{
		private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
		private readonly ILogger<UserProfileQuestionRepository> _logger;

		public UserProfileQuestionRepository(ILogger<UserProfileQuestionRepository> logger, DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
		{
			_logger = logger;
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		public async ValueTask<UserProfileQuestionEntity[]> GetQuestions()
		{
			try
			{
				return await GetContext()
					.UserProfiles
					.Where(entity => entity.Enabled == true)
					.OrderBy(entity => entity.Order)
					.ToArrayAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return null;
		}

		private DatabaseContext GetContext() => DatabaseContext.Create(_dbContextOptionsBuilder);
	}
}