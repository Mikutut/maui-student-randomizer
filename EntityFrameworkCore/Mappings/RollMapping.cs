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
	public class RollMapping : IEntityTypeConfiguration<Roll>
	{
		public const string TableName = "Rolls";

		public void Configure(EntityTypeBuilder<Roll> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.RollRefId)
				.IsUnique();

			builder.Property(x => x.RollRefId)
				.IsRequired();

			builder.Property(x => x.IndexNumber)
				.IsRequired();

			builder.Property(x => x.Value)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.HasDefaultValueSql("current_timestamp")
				.IsRequired();
		}
	}
}
