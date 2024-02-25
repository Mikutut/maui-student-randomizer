using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Change_CreationDate_Default_Values : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(4654));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(6069));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(6377));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(4654),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SchoolClassEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(2513),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 741, DateTimeKind.Utc).AddTicks(6069),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "GroupEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 15, 8, 49, 742, DateTimeKind.Utc).AddTicks(6377),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "current_timestamp");
        }
    }
}
