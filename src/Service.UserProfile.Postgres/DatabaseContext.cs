using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.UserProfile.Domain.Models;

namespace Service.UserProfile.Postgres
{
	public class DatabaseContext : MyDbContext
	{
		public const string Schema = "education";
		private const string UserProfileQuestionTableName = "userprofile_question";
		private const string UserProfileAccountTableName = "userprofile_account";

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<QuestionEntity> Questions { get; set; }
		public DbSet<AccountEntity> Accounts { get; set; }

		public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
		{
			MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

			return new DatabaseContext(options.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema);

			SetQuestionsEntityEntry(modelBuilder);
			SetAccountEntityEntry(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private static void SetQuestionsEntityEntry(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<QuestionEntity>().ToTable(UserProfileQuestionTableName);
			modelBuilder.Entity<QuestionEntity>().Property(e => e.Id).IsRequired();
			modelBuilder.Entity<QuestionEntity>().Property(e => e.Title).IsRequired();
			modelBuilder.Entity<QuestionEntity>().Property(e => e.AnswerType).IsRequired();
			modelBuilder.Entity<QuestionEntity>().Property(e => e.AnswerName);
			modelBuilder.Entity<QuestionEntity>().Property(e => e.AdditionalAnswer);
			modelBuilder.Entity<QuestionEntity>().Property(e => e.AnswerData);
			modelBuilder.Entity<QuestionEntity>().HasIndex(e => e.Id).IsUnique();
			modelBuilder.Entity<QuestionEntity>().HasIndex(e => e.Enabled);
			modelBuilder.Entity<QuestionEntity>().HasKey(e => e.Id);
		}

		private static void SetAccountEntityEntry(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccountEntity>().ToTable(UserProfileAccountTableName);
			modelBuilder.Entity<AccountEntity>().Property(e => e.UserId).IsRequired();
			modelBuilder.Entity<AccountEntity>().Property(e => e.FirstName).IsRequired();
			modelBuilder.Entity<AccountEntity>().Property(e => e.LastName).IsRequired();
			modelBuilder.Entity<AccountEntity>().Property(e => e.Gender);
			modelBuilder.Entity<AccountEntity>().Property(e => e.Phone);
			modelBuilder.Entity<AccountEntity>().Property(e => e.Country);
			modelBuilder.Entity<AccountEntity>().HasKey(e => e.UserId);
		}
	}
}