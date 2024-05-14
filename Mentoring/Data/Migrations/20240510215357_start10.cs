using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class start10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_Mentors_MentorId",
                table: "Point");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "Points");

            migrationBuilder.RenameIndex(
                name: "IX_Point_MentorId",
                table: "Points",
                newName: "IX_Points_MentorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "PointId");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatStudentUserId = table.Column<int>(type: "int", nullable: false),
                    ChatMentorUserId = table.Column<int>(type: "int", nullable: false),
                    ChatStartingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "ChatLines",
                columns: table => new
                {
                    ChatLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatLineMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatLineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatLineStudentUserId = table.Column<int>(type: "int", nullable: false),
                    ChatLineMentorUserId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLines", x => x.ChatLineId);
                    table.ForeignKey(
                        name: "FK_ChatLines_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_ChatId",
                table: "ChatLines",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Mentors_MentorId",
                table: "Points",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "MentorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Mentors_MentorId",
                table: "Points");

            migrationBuilder.DropTable(
                name: "ChatLines");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "Point");

            migrationBuilder.RenameIndex(
                name: "IX_Points_MentorId",
                table: "Point",
                newName: "IX_Point_MentorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "PointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Mentors_MentorId",
                table: "Point",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "MentorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
