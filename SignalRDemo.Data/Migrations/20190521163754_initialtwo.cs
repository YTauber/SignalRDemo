using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRDemo.Data.Migrations
{
    public partial class initialtwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "WorkingId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Done = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_WorkingId",
                table: "Jobs",
                column: "WorkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Workings_UserId",
                table: "Workings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Workings_WorkingId",
                table: "Jobs",
                column: "WorkingId",
                principalTable: "Workings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Workings_WorkingId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Workings");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_WorkingId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkingId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
