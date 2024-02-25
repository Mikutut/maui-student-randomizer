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
	public class ArchivalRollMapping : IEntityTypeConfiguration<ArchivalRoll>
	{
		public const string TableName = "ArchivalRolls";

		public void Configure(EntityTypeBuilder<ArchivalRoll> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.RollRefId)
				.IsUnique();

			builder.Property(x => x.RollRefId)
				.IsRequired();

			builder.Property(x => x.Value)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.HasDefaultValueSql("current_timestamp")
				.IsRequired();
		}
	}
}
