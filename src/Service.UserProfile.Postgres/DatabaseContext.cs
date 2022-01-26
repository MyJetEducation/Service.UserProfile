using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.UserProfile.Postgres.Models;

namespace Service.UserProfile.Postgres
{
	public class DatabaseContext : MyDbContext
	{
		public const string Schema = "education";
		private const string UserProfileAccountTableName = "userprofile_account";

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<AccountEntity> Accounts { get; set; }

		public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
		{
			MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

			return new DatabaseContext(options.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema);

			SetAccountEntityEntry(modelBuilder);

			base.OnModelCreating(modelBuilder);
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