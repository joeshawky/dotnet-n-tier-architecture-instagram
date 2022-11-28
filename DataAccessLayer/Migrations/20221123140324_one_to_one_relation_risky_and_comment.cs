using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class one_to_one_relation_risky_and_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments");

            migrationBuilder.CreateIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments",
                column: "CommentId",
                unique: true,
                filter: "[CommentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments");

            migrationBuilder.CreateIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments",
                column: "CommentId");
        }
    }
}
