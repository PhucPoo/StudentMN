using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class FixEntityCourseSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSection_Subjects_SubjectId",
                table: "CourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSection_Teachers_TeacherId",
                table: "CourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSection_CourseSection_CourseSectionId",
                table: "EnrollmentCourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSection_Students_StudentId",
                table: "EnrollmentCourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_CourseSection_CourseSectionId",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentCourseSection",
                table: "EnrollmentCourseSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSection",
                table: "CourseSection");

            migrationBuilder.RenameTable(
                name: "EnrollmentCourseSection",
                newName: "EnrollmentCourseSections");

            migrationBuilder.RenameTable(
                name: "CourseSection",
                newName: "CourseSections");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentCourseSection_CourseSectionId",
                table: "EnrollmentCourseSections",
                newName: "IX_EnrollmentCourseSections_CourseSectionId");

            migrationBuilder.RenameColumn(
                name: "CourseSectionId",
                table: "CourseSections",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSection_TeacherId",
                table: "CourseSections",
                newName: "IX_CourseSections_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSection_SubjectId",
                table: "CourseSections",
                newName: "IX_CourseSections_SubjectId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSections",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "CourseSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CourseSections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentCourseSections",
                table: "EnrollmentCourseSections",
                columns: new[] { "StudentId", "CourseSectionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSections",
                table: "CourseSections",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 9, 40, 56, 44, DateTimeKind.Local).AddTicks(7844));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 9, 40, 56, 44, DateTimeKind.Local).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 9, 40, 56, 44, DateTimeKind.Local).AddTicks(7848));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 17, 9, 40, 56, 193, DateTimeKind.Local).AddTicks(4316), "$2a$11$xWW0EE59GTZKYk63jFRMoOl8PyRxD.5QBtZFMQj1LBjSrMEcRwIam" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Subjects_SubjectId",
                table: "CourseSections",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Teachers_TeacherId",
                table: "CourseSections",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentCourseSections_CourseSections_CourseSectionId",
                table: "EnrollmentCourseSections",
                column: "CourseSectionId",
                principalTable: "CourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentCourseSections_Students_StudentId",
                table: "EnrollmentCourseSections",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_CourseSections_CourseSectionId",
                table: "Scores",
                column: "CourseSectionId",
                principalTable: "CourseSections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Subjects_SubjectId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Teachers_TeacherId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSections_CourseSections_CourseSectionId",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSections_Students_StudentId",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_CourseSections_CourseSectionId",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentCourseSections",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSections",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CourseSections");

            migrationBuilder.RenameTable(
                name: "EnrollmentCourseSections",
                newName: "EnrollmentCourseSection");

            migrationBuilder.RenameTable(
                name: "CourseSections",
                newName: "CourseSection");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentCourseSections_CourseSectionId",
                table: "EnrollmentCourseSection",
                newName: "IX_EnrollmentCourseSection_CourseSectionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CourseSection",
                newName: "CourseSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSections_TeacherId",
                table: "CourseSection",
                newName: "IX_CourseSection_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSections_SubjectId",
                table: "CourseSection",
                newName: "IX_CourseSection_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentCourseSection",
                table: "EnrollmentCourseSection",
                columns: new[] { "StudentId", "CourseSectionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSection",
                table: "CourseSection",
                column: "CourseSectionId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 10, 39, 3, 245, DateTimeKind.Local).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 10, 39, 3, 245, DateTimeKind.Local).AddTicks(1184));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 10, 39, 3, 245, DateTimeKind.Local).AddTicks(1185));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 16, 10, 39, 3, 438, DateTimeKind.Local).AddTicks(9067), "$2a$11$c6aRGLl4qsw5.sNXt0kwGOJufE8wscro3mV.IWXSbXgdOmmcpuNh2" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSection_Subjects_SubjectId",
                table: "CourseSection",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSection_Teachers_TeacherId",
                table: "CourseSection",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentCourseSection_CourseSection_CourseSectionId",
                table: "EnrollmentCourseSection",
                column: "CourseSectionId",
                principalTable: "CourseSection",
                principalColumn: "CourseSectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentCourseSection_Students_StudentId",
                table: "EnrollmentCourseSection",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_CourseSection_CourseSectionId",
                table: "Scores",
                column: "CourseSectionId",
                principalTable: "CourseSection",
                principalColumn: "CourseSectionId");
        }
    }
}
