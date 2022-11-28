using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updating_comment_risky_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RiskyComments_RiskyCommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RiskyCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RiskyCommentId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskyComments_Comments_CommentId",
                table: "RiskyComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Comments_CommentId",
                table: "RiskyComments");

            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments");

            migrationBuilder.AddColumn<int>(
                name: "RiskyCommentId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RiskyCommentId",
                table: "Comments",
                column: "RiskyCommentId",
                unique: true,
                filter: "[RiskyCommentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RiskyComments_RiskyCommentId",
                table: "Comments",
                column: "RiskyCommentId",
                principalTable: "RiskyComments",
                principalColumn: "RiskyCommentId");
        }
    }
}
