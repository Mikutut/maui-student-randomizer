﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRandomizer.EntityFrameworkCore;

#nullable disable

namespace StudentRandomizer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240225203247_RollScope_Cascade_Delete_On_SchoolClass_Or_Group_Deletion")]
    partial class RollScope_Cascade_Delete_On_SchoolClass_Or_Group_Deletion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("StudentRandomizer.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<Guid>("GroupRefId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupRefId")
                        .IsUnique();

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("StudentRandomizer.Models.GroupEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<long>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.HasIndex("StudentId", "GroupId")
                        .IsUnique();

                    b.ToTable("GroupEntries", (string)null);
                });

            modelBuilder.Entity("StudentRandomizer.Models.Roll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("IndexNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RollRefId")
                        .HasColumnType("TEXT");

                    b.Property<long>("RollScopeId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RollScopeId");

                    b.ToTable("Roll");
                });

            modelBuilder.Entity("StudentRandomizer.Models.RollScope", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<long?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RollScopeRefId")
                        .HasColumnType("TEXT");

                    b.Property<long?>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.HasIndex("SchoolClassId")
                        .IsUnique();

                    b.ToTable("RollScope");
                });

            modelBuilder.Entity("StudentRandomizer.Models.SchoolClass", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SchoolClassRefId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassRefId")
                        .IsUnique();

                    b.ToTable("SchoolClasses", (string)null);
                });

            modelBuilder.Entity("StudentRandomizer.Models.SchoolClassEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<uint>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.HasIndex("SchoolClassId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.HasIndex("StudentId", "SchoolClassId")
                        .IsUnique();

                    b.ToTable("SchoolClassEntries", (string)null);
                });

            modelBuilder.Entity("StudentRandomizer.Models.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentRefId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StudentRefId")
                        .IsUnique();

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("StudentRandomizer.Models.GroupEntry", b =>
                {
                    b.HasOne("StudentRandomizer.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRandomizer.Models.Student", "Student")
                        .WithMany("Groups")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentRandomizer.Models.Roll", b =>
                {
                    b.HasOne("StudentRandomizer.Models.RollScope", "Scope")
                        .WithMany("Rolls")
                        .HasForeignKey("RollScopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("StudentRandomizer.Models.RollScope", b =>
                {
                    b.HasOne("StudentRandomizer.Models.Group", "Group")
                        .WithOne("RollScope")
                        .HasForeignKey("StudentRandomizer.Models.RollScope", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentRandomizer.Models.SchoolClass", "SchoolClass")
                        .WithOne("RollScope")
                        .HasForeignKey("StudentRandomizer.Models.RollScope", "SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Group");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("StudentRandomizer.Models.SchoolClassEntry", b =>
                {
                    b.HasOne("StudentRandomizer.Models.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRandomizer.Models.Student", "Student")
                        .WithOne("Class")
                        .HasForeignKey("StudentRandomizer.Models.SchoolClassEntry", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentRandomizer.Models.Group", b =>
                {
                    b.Navigation("RollScope")
                        .IsRequired();

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentRandomizer.Models.RollScope", b =>
                {
                    b.Navigation("Rolls");
                });

            modelBuilder.Entity("StudentRandomizer.Models.SchoolClass", b =>
                {
                    b.Navigation("RollScope")
                        .IsRequired();

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentRandomizer.Models.Student", b =>
                {
                    b.Navigation("Class");

                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
