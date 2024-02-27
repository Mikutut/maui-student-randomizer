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
    public class AttendanceRecordMapping : IEntityTypeConfiguration<AttendanceRecord>
    {
        public const string TableName = "AttendanceRecords";

        public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.AttendanceRecordRefId)
                .IsUnique();

            builder.Property(x => x.AttendanceRecordRefId)
                .IsRequired();

            builder.Property(x => x.Date)
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

            builder.Property(x => x.IsPresent)
                .IsRequired();

            builder.HasOne(x => x.Student)
                .WithMany(y => y.Attendance)
                .HasForeignKey(x => x.StudentId)
                .IsRequired();
        }
    }
}
