using Microsoft.EntityFrameworkCore.Migrations;

namespace CheeseMVC.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Cheeses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Cheeses");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Categories");
        }
    }
}
