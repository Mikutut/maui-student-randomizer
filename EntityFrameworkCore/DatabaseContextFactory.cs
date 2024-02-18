using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.EntityFrameworkCore
{
	public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
	{
		public DatabaseContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			optionsBuilder.UseSqlite($"Data Source={AppConsts.DbPath}");

			return new DatabaseContext(optionsBuilder.Options);
		}
	}
}
