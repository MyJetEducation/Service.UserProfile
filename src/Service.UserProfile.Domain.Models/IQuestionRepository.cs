using System.Threading.Tasks;

namespace Service.UserProfile.Domain.Models
{
	public interface IQuestionRepository
	{
		ValueTask<QuestionEntity[]> GetQuestions();
	}
}