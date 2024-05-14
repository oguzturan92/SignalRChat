using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class start11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatStudentUserId",
                table: "Chats",
                newName: "ChatSenderUserId");

            migrationBuilder.RenameColumn(
                name: "ChatMentorUserId",
                table: "Chats",
                newName: "ChatReceiverUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatSenderUserId",
                table: "Chats",
                newName: "ChatStudentUserId");

            migrationBuilder.RenameColumn(
                name: "ChatReceiverUserId",
                table: "Chats",
                newName: "ChatMentorUserId");
        }
    }
}
