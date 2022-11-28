using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Edit_Risky_Comments_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments");

            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "RiskyComments");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "RiskyComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "RiskyComments",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Comments_CommentId",
                table: "RiskyComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments");

            migrationBuilder.DropIndex(
                name: "IX_RiskyComments_CommentId",
                table: "RiskyComments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "RiskyComments");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "RiskyComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "RiskyComments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskyComments_Posts_PostId",
                table: "RiskyComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
