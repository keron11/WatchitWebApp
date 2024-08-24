using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchitWebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "IsVerified", "Login", "Name", "Password", "Username" },
                values: new object[] { 1, new DateTime(2024, 7, 28, 13, 22, 6, 373, DateTimeKind.Local).AddTicks(8622), "m@gmail.com", true, "zera", "Max", "$MYHASH$V1$10000$MJkIJsIXH9WSHKBHTJitfKj88gqyIhuf+Ud++2m8ivlelC2n", "zeratul" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "IsVerified", "Login", "Name", "Password", "Username" },
                values: new object[] { 2, new DateTime(2024, 7, 28, 13, 22, 6, 379, DateTimeKind.Local).AddTicks(9526), "egor@gmail.com", false, "egazan11", "Egor", "$MYHASH$V1$10000$4BMNnYEdVkJCHb5vVv3mu35WrJosLuMySfHK/WCTOvea2gzT", "agoa" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
