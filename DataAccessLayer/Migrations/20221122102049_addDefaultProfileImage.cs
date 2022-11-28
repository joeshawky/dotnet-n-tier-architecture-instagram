using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addDefaultProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var time = DateTime.Now;
            migrationBuilder.Sql($@"INSERT INTO ProfileImages (ImageTitle, ImageExtension, ImagePath, DateTime)
                                    VALUES ('Default Picture', 'internet', 'https://www.nicepng.com/png/detail/933-9332131_profile-picture-default-png.png', '{time}');
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
