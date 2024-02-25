using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.EntityFrameworkCore.Mappings
{
	public class RollScopeMapping : IEntityTypeConfiguration<RollScope>
	{
		public const string TableName = "RollScopes";

		public void Configure(EntityTypeBuilder<RollScope> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.RollScopeRefId)
				.IsUnique();

			builder.Property(x => x.RollScopeRefId)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.HasDefaultValueSql("current_timestamp")
				.IsRequired();

			builder.HasMany(x => x.Rolls)
				.WithOne(y => y.Scope)
				.HasForeignKey(y => y.RollScopeId)
				.IsRequired();
		}
	}
}
