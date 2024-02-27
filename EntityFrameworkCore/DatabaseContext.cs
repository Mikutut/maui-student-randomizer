using Microsoft.EntityFrameworkCore;
using StudentRandomizer.EntityFrameworkCore.Mappings;
using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.EntityFrameworkCore
{
	public class DatabaseContext : DbContext
	{
		public DbSet<SchoolClass> SchoolClasses { get; set; } = null!;
		public DbSet<SchoolClassEntry> SchoolClassEntries { get; set; } = null!;
		public DbSet<Student> Students { get; set; } = null!;
		public DbSet<Group> Groups { get; set; } = null!;
		public DbSet<GroupEntry> GroupEntries { get; set; } = null!;
		public DbSet<RollScope> RollScopes { get; set; } = null!;
		public DbSet<CurrentRoll> Rolls { get; set; } = null!;
		public DbSet<ArchivalRoll> ArchivalRolls { get; set; } = null!;
		public DbSet<LuckyNumber> LuckyNumbers { get; set; } = null!;

		public DatabaseContext()
			: base()
		{

		}

		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options)
		{

		}

		public static void ConfigureDatabase(DbContextOptionsBuilder optionsBuilder)
		{
			if(!Directory.Exists(AppConsts.AppDataPath))
			{
				Directory.CreateDirectory(AppConsts.AppDataPath);
			}

			optionsBuilder.UseSqlite($"Data Source={AppConsts.DbPath}");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			ConfigureDatabase(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.ApplyConfiguration<SchoolClass>(new SchoolClassMapping())
				.ApplyConfiguration<Group>(new GroupMapping())
				.ApplyConfiguration<Student>(new StudentMapping())
				.ApplyConfiguration<SchoolClassEntry>(new SchoolClassEntryMapping())
				.ApplyConfiguration<GroupEntry>(new GroupEntryMapping())
				.ApplyConfiguration<RollScope>(new RollScopeMapping())
				.ApplyConfiguration<CurrentRoll>(new CurrentRollMapping())
				.ApplyConfiguration<ArchivalRoll>(new ArchivalRollMapping())
				.ApplyConfiguration<LuckyNumber>(new LuckyNumberMapping());
		}
	}
}
