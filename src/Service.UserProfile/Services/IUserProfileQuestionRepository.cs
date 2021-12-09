using System.Threading.Tasks;
using Service.UserProfile.Domain.Models;

namespace Service.UserProfile.Services
{
	public interface IUserProfileQuestionRepository
	{
		ValueTask<UserProfileQuestionEntity[]> GetQuestions();
	}
}