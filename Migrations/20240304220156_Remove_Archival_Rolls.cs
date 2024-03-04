using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Archival_Rolls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivalRolls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivalRolls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollScopeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "current_timestamp"),
                    RollRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivalRolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivalRolls_RollScopes_RollScopeId",
                        column: x => x.RollScopeId,
                        principalTable: "RollScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivalRolls_RollRefId",
                table: "ArchivalRolls",
                column: "RollRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivalRolls_RollScopeId",
                table: "ArchivalRolls",
                column: "RollScopeId");
        }
    }
}
