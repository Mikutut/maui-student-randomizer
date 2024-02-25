using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Split_Rolls_Into_Current_And_Archival : Migration
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

            migrationBuilder.DropTable(
                name: "Roll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollScope",
                table: "RollScope");

            migrationBuilder.RenameTable(
                name: "RollScope",
                newName: "RollScopes");

            migrationBuilder.RenameIndex(
                name: "IX_RollScope_SchoolClassId",
                table: "RollScopes",
                newName: "IX_RollScopes_SchoolClassId");

            migrationBuilder.RenameIndex(
                name: "IX_RollScope_GroupId",
                table: "RollScopes",
                newName: "IX_RollScopes_GroupId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "RollScopes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollScopes",
                table: "RollScopes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArchivalRolls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RollScopeId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "current_timestamp")
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

            migrationBuilder.CreateTable(
                name: "Rolls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RollScopeId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rolls_RollScopes_RollScopeId",
                        column: x => x.RollScopeId,
                        principalTable: "RollScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RollScopes_RollScopeRefId",
                table: "RollScopes",
                column: "RollScopeRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivalRolls_RollRefId",
                table: "ArchivalRolls",
                column: "RollRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivalRolls_RollScopeId",
                table: "ArchivalRolls",
                column: "RollScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rolls_RollRefId",
                table: "Rolls",
                column: "RollRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rolls_RollScopeId",
                table: "Rolls",
                column: "RollScopeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RollScopes_Groups_GroupId",
                table: "RollScopes",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RollScopes_SchoolClasses_SchoolClassId",
                table: "RollScopes",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollScopes_Groups_GroupId",
                table: "RollScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_RollScopes_SchoolClasses_SchoolClassId",
                table: "RollScopes");

            migrationBuilder.DropTable(
                name: "ArchivalRolls");

            migrationBuilder.DropTable(
                name: "Rolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollScopes",
                table: "RollScopes");

            migrationBuilder.DropIndex(
                name: "IX_RollScopes_RollScopeRefId",
                table: "RollScopes");

            migrationBuilder.RenameTable(
                name: "RollScopes",
                newName: "RollScope");

            migrationBuilder.RenameIndex(
                name: "IX_RollScopes_SchoolClassId",
                table: "RollScope",
                newName: "IX_RollScope_SchoolClassId");

            migrationBuilder.RenameIndex(
                name: "IX_RollScopes_GroupId",
                table: "RollScope",
                newName: "IX_RollScope_GroupId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "RollScope",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollScope",
                table: "RollScope",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollScopeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IndexNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    RollRefId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
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
    }
}
