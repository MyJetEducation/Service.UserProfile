using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.UserProfile.Postgres.Models;

namespace Service.UserProfile.Postgres
{
	public class DatabaseContext : MyDbContext
	{
		public const string Schema = "education";
		private const string UserProfileTableName = "userprofile";

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<UserProfileEntity> UserInfos { get; set; }

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
			modelBuilder.Entity<UserProfileEntity>().ToTable(UserProfileTableName);
			modelBuilder.Entity<UserProfileEntity>().HasKey(e => e.Id);

			//modelBuilder.Entity<UserProfileEntity>().Property(e => e.UserNameHash).IsRequired();

			modelBuilder.Entity<UserProfileEntity>().HasIndex(e => e.Id).IsUnique();
		}
	}
}