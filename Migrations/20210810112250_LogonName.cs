using Microsoft.EntityFrameworkCore.Migrations;

namespace HotDesk.Migrations
{
    public partial class LogonName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogonName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogonName",
                table: "User");
        }
    }
}
