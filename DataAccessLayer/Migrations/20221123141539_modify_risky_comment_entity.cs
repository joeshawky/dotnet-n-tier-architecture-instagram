using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class modify_risky_comment_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Users_UserId",
                table: "RiskyComments");

            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_UserId",
                table: "RiskyComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RiskyComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RiskyComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskyComments_UserId",
                table: "RiskyComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskyComments_Users_UserId",
                table: "RiskyComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
