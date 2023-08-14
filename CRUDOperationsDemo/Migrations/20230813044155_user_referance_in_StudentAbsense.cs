using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Migrations
{
    public partial class user_referance_in_StudentAbsense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentAbsense_StudentId",
                table: "StudentAbsense",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAbsense_User_StudentId",
                table: "StudentAbsense",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAbsense_User_StudentId",
                table: "StudentAbsense");

            migrationBuilder.DropIndex(
                name: "IX_StudentAbsense_StudentId",
                table: "StudentAbsense");
        }
    }
}
