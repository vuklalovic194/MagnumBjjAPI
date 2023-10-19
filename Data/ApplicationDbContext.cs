﻿using Magnum_web_application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;

namespace Magnum_web_application.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
			
		}
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set;}
        public DbSet<Fee> Fees { get; set; }
		public DbSet<ActiveMember> ActiveMembers { get; set; }
		public DbSet<UnpaidMonth> UnpaidMonths { get; set; }
		public DbSet<Rank> Ranks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>()
				.HasMany(m => m.TrainingSession)
				.WithOne(t => t.Member)
				.HasForeignKey(t => t.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

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
				.HasForeignKey( m => m.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UnpaidMonth>()
				.HasOne(m => m.Member)
				.WithMany(f => f.UnpaidMonth)
				.HasForeignKey(f => f.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Rank>()
				.HasOne(m => m.Member)
				.WithMany(r => r.Rank)
				.HasForeignKey(r => r.MemberId)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
