using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRDemo.Data.Migrations
{
    public partial class initialfour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nane",
                table: "Users",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Nane");
        }
    }
}
