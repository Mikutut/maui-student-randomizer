using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Add_RollScopes_And_Roles_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RollScope",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollScopeRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GroupId = table.Column<long>(type: "INTEGER", nullable: true),
                    SchoolClassId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RollScope_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RollScope_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RollScopeId = table.Column<long>(type: "INTEGER", nullable: false),
                    IndexNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roll_RollScope_RollScopeId",
                        column: x => x.RollScopeId,
                        principalTable: "RollScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roll_RollScopeId",
                table: "Roll",
                column: "RollScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScope",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScope",
                column: "SchoolClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roll");

            migrationBuilder.DropTable(
                name: "RollScope");
        }
    }
}
