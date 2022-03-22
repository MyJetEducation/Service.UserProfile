using Service.Core.Client.Constants;

namespace Service.UserProfileApi.Models
{
	public class UserStatusResponse
	{
		public UserStatusModel Status { get; set; }
	}

	public class UserStatusModel
	{
		public UserStatus Status { get; set; }

		public int? Level { get; set; }
	}
}