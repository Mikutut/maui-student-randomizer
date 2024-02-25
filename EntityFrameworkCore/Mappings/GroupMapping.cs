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
	public class GroupMapping : IEntityTypeConfiguration<Group>
	{
		string TableName = "Groups";
		
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.ToTable(TableName);

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.GroupRefId)
				.IsUnique();

			builder.Property(x => x.GroupRefId)
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.IsRequired()
				.HasDefaultValueSql("current_timestamp");

			builder.Property(x => x.ModificationDate)
				.IsRequired(false);

			builder.HasMany(x => x.Students)
				.WithOne(y => y.Group)
				.HasForeignKey(y => y.GroupId);

			builder.HasOne(x => x.RollScope)
				.WithOne(y => y.Group)
				.HasForeignKey<RollScope>(y => y.GroupId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
