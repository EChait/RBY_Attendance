using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Migrations
{
    public partial class subject_and_teacher_referances_in_StudentTeacherSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SemesterTeacherSubject_SubjectId",
                table: "SemesterTeacherSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTeacherSubject_TeacherId",
                table: "SemesterTeacherSubject",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterTeacherSubject_Subject_SubjectId",
                table: "SemesterTeacherSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterTeacherSubject_User_TeacherId",
                table: "SemesterTeacherSubject",
                column: "TeacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterTeacherSubject_Subject_SubjectId",
                table: "SemesterTeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterTeacherSubject_User_TeacherId",
                table: "SemesterTeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_SemesterTeacherSubject_SubjectId",
                table: "SemesterTeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_SemesterTeacherSubject_TeacherId",
                table: "SemesterTeacherSubject");
        }
    }
}
