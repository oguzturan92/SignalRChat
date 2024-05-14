using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class start12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatLineStudentUserId",
                table: "ChatLines",
                newName: "ChatLineSenderUserId");

            migrationBuilder.RenameColumn(
                name: "ChatLineMentorUserId",
                table: "ChatLines",
                newName: "ChatLineReceiverUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatLineSenderUserId",
                table: "ChatLines",
                newName: "ChatLineStudentUserId");

            migrationBuilder.RenameColumn(
                name: "ChatLineReceiverUserId",
                table: "ChatLines",
                newName: "ChatLineMentorUserId");
        }
    }
}
