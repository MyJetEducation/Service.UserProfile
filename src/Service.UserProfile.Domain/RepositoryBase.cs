using Microsoft.EntityFrameworkCore;
using Service.UserProfile.Postgres;

namespace Service.UserProfile.Domain
{
	public abstract class RepositoryBase
	{
		private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

		protected RepositoryBase(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder) => _dbContextOptionsBuilder = dbContextOptionsBuilder;

		protected DatabaseContext GetContext() => DatabaseContext.Create(_dbContextOptionsBuilder);
	}
}