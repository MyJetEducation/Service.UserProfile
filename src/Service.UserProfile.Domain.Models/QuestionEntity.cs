namespace Service.UserProfile.Domain.Models
{
	public class QuestionEntity
	{
		public int? Id { get; set; }

		public string Title { get; set; }

		public string AnswerType { get; set; }

		public string AnswerName { get; set; }

		public bool? AdditionalAnswer { get; set; }

		public string AnswerData { get; set; }

		public int? Order { get; set; }

		public bool? Enabled { get; set; }
	}
}