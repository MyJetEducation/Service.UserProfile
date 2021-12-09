using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Postgres;

namespace Service.UserProfile.Domain
{
	public class AccountRepository : RepositoryBase, IAccountRepository
	{
		private readonly ILogger<AccountRepository> _logger;

		public AccountRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder, ILogger<AccountRepository> logger) :
			base(dbContextOptionsBuilder) => _logger = logger;

		public async ValueTask<AccountEntity> GetAccount(Guid? userId)
		{
			try
			{
				return await GetEntity(userId);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return await ValueTask.FromResult<AccountEntity>(null);
		}

		private async Task<AccountEntity> GetEntity(Guid? userId) => await GetContext()
			.Accounts
			.Where(entity => entity.UserId == userId)
			.FirstOrDefaultAsync();

		public async ValueTask<bool> SaveAccount(AccountEntity entity)
		{
			try
			{
				AccountEntity existingAccount = await GetEntity(entity.UserId);

				DatabaseContext context = GetContext();

				if (existingAccount == null)
					await context.Accounts.AddAsync(entity);
				else
					context.Update(entity);

				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return false;
		}
	}
}