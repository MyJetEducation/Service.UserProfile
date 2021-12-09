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

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<UserProfileQuestionEntity> UserProfiles { get; set; }

		public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
		{
			MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

			return new DatabaseContext(options.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema);

			SetUserInfoEntityEntry(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private static void SetUserInfoEntityEntry(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserProfileQuestionEntity>().ToTable(UserProfileQuestionTableName);
			modelBuilder.Entity<UserProfileQuestionEntity>().Property(e => e.Title).IsRequired();
			modelBuilder.Entity<UserProfileQuestionEntity>().Property(e => e.AnswerType).IsRequired();
			modelBuilder.Entity<UserProfileQuestionEntity>().Property(e => e.AnswerName);
			modelBuilder.Entity<UserProfileQuestionEntity>().Property(e => e.AdditionalAnswer);
			modelBuilder.Entity<UserProfileQuestionEntity>().Property(e => e.AnswerData);
			modelBuilder.Entity<UserProfileQuestionEntity>().HasIndex(e => e.Id).IsUnique();
			modelBuilder.Entity<UserProfileQuestionEntity>().HasKey(e => e.Id);
		}
	}
}