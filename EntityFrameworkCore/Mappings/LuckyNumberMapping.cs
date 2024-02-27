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
    public class LuckyNumberMapping : IEntityTypeConfiguration<LuckyNumber>
    {
        public const string TableName = "LuckyNumbers";

        public void Configure(EntityTypeBuilder<LuckyNumber> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.LuckyNumberRefId)
                .IsUnique();

            builder.Property(x => x.LuckyNumberRefId)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();
        }
    }
}
