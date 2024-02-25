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
	public class SchoolClassMapping : IEntityTypeConfiguration<SchoolClass>
	{
		string TableName = "SchoolClasses";

		public void Configure(EntityTypeBuilder<SchoolClass> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.SchoolClassRefId)
				.IsUnique();

			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.SchoolClassRefId)
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.IsRequired()
				.HasDefaultValueSql("current_timestamp");

			builder.Property(x => x.ModificationDate)
				.IsRequired(false);

			builder.HasOne(x => x.RollScope)
				.WithOne(y => y.SchoolClass)
				.HasForeignKey<RollScope>(y => y.SchoolClassId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
