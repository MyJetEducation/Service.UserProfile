using Service.Core.Client.Constants;

namespace Service.WalletApi.UserProfileApi.Controllers.Contracts
{
	public class UserStatusResponse
	{
		public UserStatus Status { get; set; }

		public int? Level { get; set; }
	}
}