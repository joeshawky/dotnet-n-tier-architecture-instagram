using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class remove_risky_comment_from_post_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments");

            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_PostId",
                table: "RiskyComments");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "RiskyComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "RiskyComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskyComments_PostId",
                table: "RiskyComments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }
    }
}
