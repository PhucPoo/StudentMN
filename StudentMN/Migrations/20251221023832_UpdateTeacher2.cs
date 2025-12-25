using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeacher2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}