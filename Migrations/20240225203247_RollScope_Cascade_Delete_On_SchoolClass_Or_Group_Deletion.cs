using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class RollScope_Cascade_Delete_On_SchoolClass_Or_Group_Deletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollScope_Groups_GroupId",
                table: "RollScope");

            migrationBuilder.DropForeignKey(
                name: "FK_RollScope_SchoolClasses_SchoolClassId",
                table: "RollScope");

            migrationBuilder.DropIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScope");

            migrationBuilder.DropIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScope");

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScope",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScope",
                column: "SchoolClassId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RollScope_Groups_GroupId",
                table: "RollScope",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RollScope_SchoolClasses_SchoolClassId",
                table: "RollScope",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollScope_Groups_GroupId",
                table: "RollScope");

            migrationBuilder.DropForeignKey(
                name: "FK_RollScope_SchoolClasses_SchoolClassId",
                table: "RollScope");

            migrationBuilder.DropIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScope");

            migrationBuilder.DropIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScope");

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScope",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScope",
                column: "SchoolClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_RollScope_Groups_GroupId",
                table: "RollScope",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RollScope_SchoolClasses_SchoolClassId",
                table: "RollScope",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }
    }
}
