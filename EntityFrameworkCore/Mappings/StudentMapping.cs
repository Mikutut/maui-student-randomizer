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
	public class StudentMapping : IEntityTypeConfiguration<Student>
	{
		string TableName = "Students";

		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.StudentRefId)
				.IsUnique();

			builder.Property(x => x.StudentRefId)
				.IsRequired()
				.HasDefaultValue(Guid.NewGuid());

			builder.Property(x => x.FirstName)
				.IsRequired();

			builder.Property(x => x.LastName)
				.IsRequired();
		}
	}
}
