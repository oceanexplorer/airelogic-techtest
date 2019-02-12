using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTraq.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 75, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugId);
                    table.ForeignKey(
                        name: "FK_Bugs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "Surname" },
                values: new object[] { 1, "Jeff", "Simms" });

            migrationBuilder.InsertData(
                table: "Bugs",
                columns: new[] { "BugId", "CreatedDate", "Description", "Status", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2019, 2, 12, 23, 9, 35, 741, DateTimeKind.Local).AddTicks(90), "This is our first bug!", "Open", "First Bug", 1 });

            migrationBuilder.InsertData(
                table: "Bugs",
                columns: new[] { "BugId", "CreatedDate", "Description", "Status", "Title", "UserId" },
                values: new object[] { 2, new DateTime(2019, 2, 12, 23, 9, 35, 745, DateTimeKind.Local).AddTicks(4840), "This is our second bug!", "Open", "Second Bud", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_UserId",
                table: "Bugs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
