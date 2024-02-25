using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Guid_Columns_Default_Value : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StudentRefId",
                table: "Students",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("6bb33023-0150-4d42-b5bb-5e9384503918"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolClassRefId",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("e8b48984-c518-4c32-93c0-59c01adb00b2"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(4654),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 327, DateTimeKind.Utc).AddTicks(8710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(2513),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(8965));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupRefId",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("51220e17-bc88-4166-b89d-678ae3d7232d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(6069),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(6377),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 329, DateTimeKind.Utc).AddTicks(3922));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StudentRefId",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("6bb33023-0150-4d42-b5bb-5e9384503918"),
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolClassRefId",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("e8b48984-c518-4c32-93c0-59c01adb00b2"),
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 327, DateTimeKind.Utc).AddTicks(8710),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(4654));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(8965),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupRefId",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("51220e17-bc88-4166-b89d-678ae3d7232d"),
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 328, DateTimeKind.Utc).AddTicks(636),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(6069));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 6, 50, 329, DateTimeKind.Utc).AddTicks(3922),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(6377));
        }
    }
}
