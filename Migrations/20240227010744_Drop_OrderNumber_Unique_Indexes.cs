using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Drop_OrderNumber_Unique_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchoolClassEntries_OrderNumber",
                table: "SchoolClassEntries");

            migrationBuilder.DropIndex(
                name: "IX_GroupEntries_OrderNumber",
                table: "GroupEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassEntries_OrderNumber",
                table: "SchoolClassEntries",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntries_OrderNumber",
                table: "GroupEntries",
                column: "OrderNumber",
                unique: true);
        }
    }
}
