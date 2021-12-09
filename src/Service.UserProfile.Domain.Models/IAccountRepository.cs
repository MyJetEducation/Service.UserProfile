using System;
using System.Threading.Tasks;

namespace Service.UserProfile.Domain.Models
{
	public interface IAccountRepository
	{
		ValueTask<bool> SaveAccount(AccountEntity entity);

		ValueTask<AccountEntity> GetAccount(Guid? userId);
	}
}