using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi_Projekt.Migrations
{
    public partial class DatabaseV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "GeoMessages",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "GeoMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "GeoMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "GeoMessages");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "GeoMessages");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "GeoMessages",
                newName: "Message");
        }
    }
}
