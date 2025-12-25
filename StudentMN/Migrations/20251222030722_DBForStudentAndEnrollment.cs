using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class DBForStudentAndEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSections_CourseSections_CourseSectionId",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentCourseSections_Students_StudentId",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentCourseSections",
                table: "EnrollmentCourseSections");

            migrationBuilder.RenameTable(
                name: "EnrollmentCourseSections",
                newName: "Enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentCourseSections_CourseSectionId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseSectionId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 10, 7, 22, 159, DateTimeKind.Local).AddTicks(2855));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 10, 7, 22, 159, DateTimeKind.Local).AddTicks(2857));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 10, 7, 22, 159, DateTimeKind.Local).AddTicks(2859));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 22, 10, 7, 22, 323, DateTimeKind.Local).AddTicks(3202), "$2a$11$OV4y9VgFGaH1PHB/RiayCeP86JghgzhnN6hVY.WqEyAnN1bfP.P2e" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_CourseSections_CourseSectionId",
                table: "Enrollments",
                column: "CourseSectionId",
                principalTable: "CourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_CourseSections_CourseSectionId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "EnrollmentCourseSections");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseSectionId",
                table: "EnrollmentCourseSections",
                newName: "IX_EnrollmentCourseSections_CourseSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentCourseSections",
                table: "EnrollmentCourseSections",
                columns: new[] { "StudentId", "CourseSectionId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 9, 38, 32, 378, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 9, 38, 32, 378, DateTimeKind.Local).AddTicks(8390));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 9, 38, 32, 378, DateTimeKind.Local).AddTicks(8392));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 21, 9, 38, 32, 492, DateTimeKind.Local).AddTicks(1925), "$2a$11$vXDg.bMMFZ96D2SXexwem.1ixPwgMJDdyceDwJXGulfhI9d7huiKC" });

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
        }
    }
}