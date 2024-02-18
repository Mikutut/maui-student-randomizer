using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRandomizer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupRefId = table.Column<Guid>(type: "TEXT", nullable: false, defaultValue: new Guid("4d16e513-6d15-45c6-9855-40a0299106bd")),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(2319)),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClasses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolClassRefId = table.Column<Guid>(type: "TEXT", nullable: false, defaultValue: new Guid("05fad3ed-a3f4-4f0a-94d3-f0e1311ec78e")),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(902)),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentRefId = table.Column<Guid>(type: "TEXT", nullable: false, defaultValue: new Guid("977ee329-bfa0-4c19-9466-9b7a5cc7a5b9")),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<long>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<uint>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 617, DateTimeKind.Utc).AddTicks(1545))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupEntries_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEntries_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClassEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolClassId = table.Column<long>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<long>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<uint>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 2, 18, 23, 45, 21, 616, DateTimeKind.Utc).AddTicks(8130))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolClassEntries_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClassEntries_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntries_GroupId",
                table: "GroupEntries",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntries_OrderNumber",
                table: "GroupEntries",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntries_StudentId_GroupId",
                table: "GroupEntries",
                columns: new[] { "StudentId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupRefId",
                table: "Groups",
                column: "GroupRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassEntries_OrderNumber",
                table: "SchoolClassEntries",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassEntries_SchoolClassId",
                table: "SchoolClassEntries",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassEntries_StudentId",
                table: "SchoolClassEntries",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassEntries_StudentId_SchoolClassId",
                table: "SchoolClassEntries",
                columns: new[] { "StudentId", "SchoolClassId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_SchoolClassRefId",
                table: "SchoolClasses",
                column: "SchoolClassRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentRefId",
                table: "Students",
                column: "StudentRefId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupEntries");

            migrationBuilder.DropTable(
                name: "SchoolClassEntries");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
