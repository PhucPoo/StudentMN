using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class InitStudentClassMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6372));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6374));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6378));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6379));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 759, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 759, DateTimeKind.Local).AddTicks(5914));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 10, 3, 21, 759, DateTimeKind.Local).AddTicks(5916));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 10, 10, 3, 21, 872, DateTimeKind.Local).AddTicks(6105), "$2a$11$IDCYsd8Ozg52seQ.OcNuTuxjmcqDTEHt9jCsUZTlUAReNWS4Fb6OG" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_MajorId",
                table: "Teachers",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_MajorId",
                table: "Subjects",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_StudentId",
                table: "Scores",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SubjectId",
                table: "Scores",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_MajorId",
                table: "Classes",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Majors_MajorId",
                table: "Classes",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Students_StudentId",
                table: "Scores",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Subjects_SubjectId",
                table: "Scores",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Majors_MajorId",
                table: "Subjects",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Majors_MajorId",
                table: "Teachers",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Majors_MajorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Students_StudentId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Subjects_SubjectId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Majors_MajorId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Majors_MajorId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_MajorId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_MajorId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Scores_StudentId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_SubjectId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Classes_MajorId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(638));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(640));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(679));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(681));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(683));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(685));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(687));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 51, 901, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 51, 901, DateTimeKind.Local).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 9, 27, 51, 901, DateTimeKind.Local).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 27, 52, 71, DateTimeKind.Local).AddTicks(322), "$2a$11$P2puuKG4BngjeV3S97n7n.H/QpbwCThH2xPAi2ZxRELyYb55kMEx2" });
        }
    }
}
