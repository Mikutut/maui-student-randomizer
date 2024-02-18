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
	public class SchoolClassEntryMapping : IEntityTypeConfiguration<SchoolClassEntry>
	{
		string TableName = "SchoolClassEntries";

		public void Configure(EntityTypeBuilder<SchoolClassEntry> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.SchoolClass)
				.WithMany(y => y.Students)
				.HasForeignKey(y => y.SchoolClassId)
				.IsRequired();

			builder.HasOne(x => x.Student)
				.WithOne(y => y.Class)
				.HasForeignKey<SchoolClassEntry>(x => x.StudentId)
				.IsRequired();

			builder.Property(x => x.OrderNumber)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.IsRequired()
				.HasDefaultValue(DateTime.UtcNow);

			builder.HasIndex(x => x.OrderNumber)
				.IsUnique();

			builder.HasIndex(x => new { x.StudentId, x.SchoolClassId })
				.IsUnique();
		}
	}
}
