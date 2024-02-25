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
	public class GroupEntryMapping : IEntityTypeConfiguration<GroupEntry>
	{
		string TableName = "GroupEntries";

		public void Configure(EntityTypeBuilder<GroupEntry> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Group)
				.WithMany(y => y.Students)
				.HasForeignKey(x => x.GroupId)
				.IsRequired();

			builder.HasOne(x => x.Student)
				.WithMany(y => y.Groups)
				.HasForeignKey(x => x.StudentId)
				.IsRequired();

			builder.Property(x => x.OrderNumber)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.IsRequired()
				.HasDefaultValueSql("current_timestamp");

			builder.HasIndex(x => x.OrderNumber)
				.IsUnique();

			builder.HasIndex(x => new { x.StudentId, x.GroupId })
				.IsUnique();
		}
	}
}
