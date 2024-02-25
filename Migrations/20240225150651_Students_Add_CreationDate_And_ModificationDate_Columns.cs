using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Students_Add_CreationDate_And_ModificationDate_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StudentRefId",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("6bb33023-0150-4d42-b5bb-5e9384503918"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("977ee329-bfa0-4c19-9466-9b7a5cc7a5b9"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolClassRefId",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("e8b48984-c518-4c32-93c0-59c01adb00b2"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("05fad3ed-a3f4-4f0a-94d3-f0e1311ec78e"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 327, DateTimeKind.Utc).AddTicks(8710),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(8965),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(8130));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupRefId",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("51220e17-bc88-4166-b89d-678ae3d7232d"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("4d16e513-6d15-45c6-9855-40a0299106bd"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(636),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(2319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 329, DateTimeKind.Utc).AddTicks(3922),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 617, DateTimeKind.Utc).AddTicks(1545));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentRefId",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("977ee329-bfa0-4c19-9466-9b7a5cc7a5b9"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("6bb33023-0150-4d42-b5bb-5e9384503918"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolClassRefId",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("05fad3ed-a3f4-4f0a-94d3-f0e1311ec78e"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("e8b48984-c518-4c32-93c0-59c01adb00b2"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(902),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 327, DateTimeKind.Utc).AddTicks(8710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(8130),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(8965));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupRefId",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("4d16e513-6d15-45c6-9855-40a0299106bd"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("51220e17-bc88-4166-b89d-678ae3d7232d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(2319),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 617, DateTimeKind.Utc).AddTicks(1545),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 329, DateTimeKind.Utc).AddTicks(3922));
        }
    }
}
