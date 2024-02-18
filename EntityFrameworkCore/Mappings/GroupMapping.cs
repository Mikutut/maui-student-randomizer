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
				.IsRequired()
				.HasDefaultValue(Guid.NewGuid());

			builder.Property(x => x.Name)
				.IsRequired();

			builder.Property(x => x.CreationDate)
				.IsRequired()
				.HasDefaultValue(DateTime.UtcNow);

			builder.Property(x => x.ModificationDate)
				.IsRequired(false);

			builder.HasMany(x => x.Students)
				.WithOne(y => y.Group)
				.HasForeignKey(y => y.GroupId);
		}
	}
}
