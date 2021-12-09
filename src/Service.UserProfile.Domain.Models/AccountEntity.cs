using System;

namespace Service.UserProfile.Domain.Models
{
	public class AccountEntity
	{
		public Guid? UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Gender { get; set; }

		public string Phone { get; set; }

		public string Country { get; set; }
	}
}