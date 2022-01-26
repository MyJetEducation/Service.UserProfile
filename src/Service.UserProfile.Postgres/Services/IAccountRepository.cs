using Service.UserProfile.Postgres.Models;

namespace Service.UserProfile.Postgres.Services
{
	public interface IAccountRepository
	{
		ValueTask<bool> SaveAccount(AccountEntity entity);

		ValueTask<AccountEntity> GetAccount(Guid? userId);
	}
}