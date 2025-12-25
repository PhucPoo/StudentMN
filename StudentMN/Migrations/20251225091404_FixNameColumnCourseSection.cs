using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class FixNameColumnCourseSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remaining",
                table: "CourseSections",
                newName: "Remainning");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "CourseSections",
                newName: "GroupNumber");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 14, 3, 982, DateTimeKind.Local).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 14, 3, 982, DateTimeKind.Local).AddTicks(796));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 14, 3, 982, DateTimeKind.Local).AddTicks(797));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 14, 4, 147, DateTimeKind.Local).AddTicks(820), "$2a$11$aE7dSPScCU3SaIq39z3WmudUls9SBWU1FjokZZctWmmNQMuvzZux." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remainning",
                table: "CourseSections",
                newName: "Remaining");

            migrationBuilder.RenameColumn(
                name: "GroupNumber",
                table: "CourseSections",
                newName: "Group");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 9, 39, 525, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 9, 39, 525, DateTimeKind.Local).AddTicks(6864));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 9, 39, 525, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 9, 39, 699, DateTimeKind.Local).AddTicks(8383), "$2a$11$Uba74l31dY6Ie4MnpMA1z.QKzRvdmKw5tzqTDUEPu7Kmy2MUKGtU2" });
        }
    }
}
