using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "Course");

            migrationBuilder.RenameColumn(
                name: "RegisteredAt",
                table: "EnrollmentCourseSections",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EnrollmentCourseSections",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EnrollmentCourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "EnrollmentCourseSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 8, 35, 37, 526, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 8, 35, 37, 526, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 8, 35, 37, 526, DateTimeKind.Local).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 21, 8, 35, 37, 719, DateTimeKind.Local).AddTicks(9402), "$2a$11$mw8rNWZQ8iK/PyaocZPBAOxyamEt6mxOy8hy/R0nkXccFSITbtkta" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EnrollmentCourseSections");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "EnrollmentCourseSections");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Students",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EnrollmentCourseSections",
                newName: "RegisteredAt");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
