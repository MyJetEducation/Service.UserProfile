using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Postgres;

namespace Service.UserProfile.Domain
{
	public class QuestionRepository : RepositoryBase, IQuestionRepository
	{
		private readonly ILogger<QuestionRepository> _logger;

		public QuestionRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder, ILogger<QuestionRepository> logger) :
			base(dbContextOptionsBuilder) => _logger = logger;

		public async ValueTask<QuestionEntity[]> GetQuestions()
		{
			try
			{
				return await GetContext()
					.Questions
					.Where(entity => entity.Enabled == true)
					.OrderBy(entity => entity.Order)
					.ToArrayAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return await ValueTask.FromResult<QuestionEntity[]>(null);
		}
	}
}