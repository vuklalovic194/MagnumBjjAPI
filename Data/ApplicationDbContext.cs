using Magnum_API_web_application.Models;
using Microsoft.EntityFrameworkCore;

namespace Magnum_API_web_application.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<TrainingSession> TrainingSessions { get; set; }
		public DbSet<Fee> Fees { get; set; }
		public DbSet<ActiveMember> ActiveMembers { get; set; }
		public DbSet<UnpaidMonth> UnpaidMonths { get; set; }
		public DbSet<Rank> Ranks { get; set; }
		public DbSet<Competition> Competitions { get; set; }
		public DbSet<Competition_Member_Result> Competitions_Members_Results { get; set; }
		public DbSet<CompetitionResult> CompetitionResults { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TrainingSession>()
				.HasOne(m => m.Member)
				.WithMany(t => t.TrainingSession)
				.HasForeignKey(t => t.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Fee>()
				.HasOne(m => m.Member)
				.WithMany(f => f.Fee)
				.HasForeignKey(f => f.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ActiveMember>()
				.HasOne(m => m.Member)
				.WithMany(a => a.ActiveMember)
				.HasForeignKey(m => m.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UnpaidMonth>()
				.HasOne(m => m.Member)
				.WithMany(f => f.UnpaidMonth)
				.HasForeignKey(f => f.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Rank>()
				.HasMany(r => r.Members)
				.WithOne(m => m.Rank)
				.HasForeignKey(m => m.RankId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Competition_Member_Result>()
				.HasKey("Id", "MemberId", "CompetitionId", "ResultId");

			modelBuilder.Entity<Competition_Member_Result>()
				.HasOne(m => m.Member)
				.WithMany(c => c.Competition_Member_Result)
				.HasForeignKey(c => c.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Competition_Member_Result>()
				.HasOne(m => m.Competition)
				.WithMany(c => c.Competition_Member_Result)
				.HasForeignKey(c => c.CompetitionId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Competition_Member_Result>()
				.HasOne(c => c.CompetitionResult)
				.WithMany(c => c.Competition_Member_Result)
				.HasForeignKey(c => c.ResultId)
				.OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<Competition>()
			//	.HasMany(u => u.Members)
			//	.WithMany(u => u.Competitions);
		}
	}
}
