using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class CourseSectionFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Subjects_SubjectId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_SubjectId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Scores");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 26, 13, 55, 53, 20, DateTimeKind.Local).AddTicks(401));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 26, 13, 55, 53, 20, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 26, 13, 55, 53, 20, DateTimeKind.Local).AddTicks(404));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 26, 13, 55, 53, 184, DateTimeKind.Local).AddTicks(1599), "$2a$11$d.q1pGvs8bBd7QebX8woouklnIDUIvIDEmgI611LzOm48HYCZfgdq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 28, 39, 265, DateTimeKind.Local).AddTicks(8274));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 28, 39, 265, DateTimeKind.Local).AddTicks(8276));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 16, 28, 39, 265, DateTimeKind.Local).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 28, 39, 438, DateTimeKind.Local).AddTicks(396), "$2a$11$/KraPf/1rWvncxqcGPHfReN0WXLkkidoPcvf9sGo5rsaYghHJk0bm" });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SubjectId",
                table: "Scores",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Subjects_SubjectId",
                table: "Scores",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
